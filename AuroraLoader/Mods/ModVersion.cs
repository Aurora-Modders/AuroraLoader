using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Semver;

namespace AuroraLoader.Mods
{
    public class ModVersion
    {
        [JsonPropertyName("version")]
        [JsonConverter(typeof(SemVersionJsonConverter))]
        public SemVersion Version { get; set; }

        [JsonPropertyName("target_aurora_version")]
        [JsonConverter(typeof(ModCompatibilityVersionJsonConverter))]
        public ModCompabitilityVersion TargetAuroraVersion { get; set; }

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }

        [JsonIgnore]
        public bool Downloaded { get; set; } = false;

        [JsonIgnore]
        public string DownloadPath => Path.Combine(Mod.ModFolder, Version.ToString());

        [JsonIgnore]
        public Mod Mod { get; set; }


        public void Download()
        {
            if (Downloaded)
            {
                throw new Exception($"{Mod.Name} {Version} is already installed");
            }

            Log.Debug($"Preparing caches in {Program.CacheDirectory}");
            var zip = Path.Combine(Program.ModDirectory, "update.current");
            if (File.Exists(zip))
            {
                File.Delete(zip);
            }

            var extract_folder = Path.Combine(Program.CacheDirectory, "Extract");
            if (Directory.Exists(extract_folder))
            {
                Directory.Delete(extract_folder, true);
            }
            Directory.CreateDirectory(extract_folder);

            Log.Debug($"Downloading from {DownloadUrl}");
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(DownloadUrl, zip);
                }

                ZipFile.ExtractToDirectory(zip, extract_folder);

                if (Directory.Exists(DownloadPath))
                {
                    Directory.Delete(DownloadPath, true);
                }
                Directory.CreateDirectory(DownloadPath);

                ZipFile.ExtractToDirectory(zip, DownloadPath);

                Downloaded = true;
                Mod.UpdateCache();
            }
            catch (Exception e)
            {
                var message = $"Failed while installing or updating {Mod.Name} {Version} to {DownloadPath} from {DownloadUrl}";
                Log.Error(message, e);
                MessageBox.Show(message);
            }
            finally
            {
                // Cleanup
                File.Delete(zip);
                Directory.Delete(extract_folder, true);
            }
        }

        public void Install(AuroraInstallation installation)
        {
            Log.Debug($"{Mod.Type}: {Mod.Name} {Version}");
            if (Mod.Type == ModType.DATABASE)
            {
                InstallDbMod(installation);
            }
            else if (Mod.Type == ModType.ROOTUTILITY || Mod.Type == ModType.EXECUTABLE || Mod.Type == ModType.THEME)
            {
                MoveToRootFolder();
            }
        }

        public void Uninstall(AuroraInstallation installation)
        {
            if (Mod.Type == ModType.THEME)
            {
                UninstallThemeMod();
            }
            else if (Mod.Type == ModType.DATABASE)
            {
                UninstallDbMod(installation);
            }
        }

        internal void InstallDbMod(AuroraInstallation installation)
        {
            const string TABLE = "CREATE TABLE IF NOT EXISTS A_THIS_SAVE_IS_MODDED (ModName Text PRIMARY KEY);";

            using (var connection = new SQLiteConnection(installation.ConnectionString))
            {
                connection.Open();
                var table = new SQLiteCommand(TABLE, connection);
                table.ExecuteNonQuery();

                var files = Directory.EnumerateFiles(DownloadPath, "*.sql").ToList();
                try
                {
                    var uninstall = files.Single(f => Path.GetFileName(f).Equals("uninstall.sql"));
                    files.Remove(uninstall);
                }
                catch (Exception)
                {
                    Log.Debug($"No uninstall for db mod: {Mod.Name}");
                }

                foreach (var file in files)
                {
                    var sql = File.ReadAllText(file);
                    var command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();

                    sql = $"INSERT INTO A_THIS_SAVE_IS_MODDED(ModName)" +
                            $"SELECT '{Mod.Name}'" +
                            $"WHERE NOT EXISTS(SELECT 1 FROM A_THIS_SAVE_IS_MODDED WHERE ModName = '{Mod.Name}');";

                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();

                    Log.Debug($"Installed db mod: {Mod.Name}");
                }

                connection.Close();
            }
        }

        internal void UninstallDbMod(AuroraInstallation installation)
        {
            var uninstallScript = Path.Combine(DownloadPath, "uninstall.sql");
            if (!File.Exists(uninstallScript))
            {
                throw new Exception($"Db mod {Mod.Name} uninstall.sql not found at {DownloadPath}");
            }

            using (var connection = new SQLiteConnection(installation.ConnectionString))
            {
                connection.Open();

                var sql = "SELECT * FROM sqlite_master WHERE name ='A_THIS_SAVE_IS_MODDED' and type='table';";
                var command = new SQLiteCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    Log.Debug($"Not attempting to uninstall {Mod.Name} since {sql} returned no rows");
                    return;
                }
                else
                {
                    reader.Close();
                }

                sql = $"SELECT * FROM A_THIS_SAVE_IS_MODDED WHERE ModName = '{Mod.Name}'";
                command = new SQLiteCommand(sql, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader[0].ToString();
                    if (name == null)
                    {
                        throw new Exception($"Installed db mod {Mod.Name} not found");
                    }
                    else
                    {
                        sql = File.ReadAllText(uninstallScript);
                        sql += $"\nDELETE FROM A_THIS_SAVE_IS_MODDED\nWHERE ModName = '{name}';";
                        command = new SQLiteCommand(sql, connection);
                        command.ExecuteNonQuery();

                        Log.Debug($"Uninstalled db mod: {name}");
                    }
                }

                reader.Close();
                connection.Close();
            }
        }

        internal void UninstallThemeMod()
        {
            if (!Downloaded)
            {
                Log.Debug($"UninstallThemeMod called for {Mod.Name} {Version}, but it is not installed");
            }

            foreach (var fileInMod in Directory.EnumerateFiles(DownloadPath, "*.*", SearchOption.AllDirectories))
            {
                var out_file = Path.Combine(Program.AuroraLoaderExecutableDirectory, Path.GetRelativePath(DownloadPath, fileInMod));
                if (File.Exists(out_file))
                {
                    File.Delete(out_file);
                }
            }

            Log.Debug($"Uninstalled theme mod: {Mod.Name}");
        }

        private void MoveToRootFolder()
        {
            foreach (var file in Directory.EnumerateFiles(DownloadPath, "*.*", SearchOption.AllDirectories).Where(f => !Path.GetFileName(f).Equals("mod.json")))
            {
                File.Copy(file, Path.Combine(Program.AuroraLoaderExecutableDirectory, Path.GetFileName(file)), true);
            }
        }

        public Process Run()
        {
            if (Mod.Type == ModType.UTILITY)
            {
                return Run(DownloadPath, Mod.LaunchCommand);
            }
            else if (Mod.Type == ModType.ROOTUTILITY || Mod.Type == ModType.EXECUTABLE)
            {
                return Run(Program.AuroraLoaderExecutableDirectory, Mod.LaunchCommand);
            }
            else
            {
                throw new InvalidOperationException($"{Mod.Type} does not support being run");
            }
        }

        private static Process Run(string folder, string command)
        {
            if (!Directory.Exists(folder))
            {
                throw new FileNotFoundException(folder);
            }
            //var java = Environment.GetEnvironmentVariable("PROGRAMFILES(X86)") + @"\Common Files\Oracle\Java\javapath";

            Log.Debug($"Running command: {command} in folder: {folder}");
            var pieces = command.Split(' ');
            var exe = pieces[0];
            var args = "";
            if (pieces.Length > 1)
            {
                for (int i = 1; i < pieces.Length; i++)
                {
                    args += " " + pieces[i];
                }

                args = args.Substring(1);
            }

            Log.Debug("Running: " + command);
            if (!exe.ToLower().Equals("java"))
            {
                exe = Path.Combine(folder, exe);
            }

            var processStartInfo = new ProcessStartInfo()
            {
                WorkingDirectory = folder,
                FileName = exe,
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            //info.EnvironmentVariables["PATH"] = java + ";" + Environment.GetEnvironmentVariable("PATH");

            try
            {
                var process = Process.Start(processStartInfo);
                return process;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to run {command}", e);
                return null;
            }

        }
    }
}
