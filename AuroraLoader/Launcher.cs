using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AuroraLoader.Mods;
using AuroraLoader.Registry;

namespace AuroraLoader
{
    static class Launcher
    {
        public static string GetConnectionString(GameInstallation installation)
        {
            var db = Path.Combine(installation.InstallationPath, "AuroraDB.db");

            return $"Data Source={db};Version=3;New=False;Compress=True;";
        }

        public static List<Process> Launch(GameInstallation installation, ModRegistry registry, IList<Mod> mods, Mod executableMod = null)
        {
            Log.Debug($"Launching from {installation.InstallationPath}");
            if (mods.Any(mod => mod.Type == ModType.EXE))
            {
                throw new Exception("Use the other parameter");
            }

            UninstallThemeMods(installation, registry);
            UninstallDbMods(installation, registry);

            var processes = new List<Process>();

            foreach (var mod in mods.Where(mod => mod.Type == ModType.THEME))
            {
                Log.Debug($"Theme: {mod.Name}");
                InstallThemeMod(mod, installation);
            }
            foreach (var mod in mods.Where(mod => mod.Type == ModType.DATABASE))
            {
                Log.Debug("Database: " + mod.Name);
                InstallDbMod(mod, installation);
            }
            foreach (var mod in mods.Where(mod => mod.Type == ModType.ROOTUTILITY))
            {
                Log.Debug("Root Utility: " + mod.Name);
                CopyToFolder(mod, installation.InstallationPath);
                var process = Run(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), mod.Installation.ExecuteCommand);
                processes.Add(process);
            }
            foreach (var mod in mods.Where(mod => mod.Type == ModType.UTILITY))
            {
                Log.Debug("Utility: " + mod.Name);
                var process = Run(mod.Installation.ModFolder, mod.Installation.ExecuteCommand);
                processes.Add(process);
            }
            
            if (executableMod != null)
            {
                CopyToFolder(executableMod, installation.InstallationPath);
                var process = Run(Program.AuroraLoaderExecutableDirectory, executableMod.Installation.ExecuteCommand);
                processes.Insert(0, process);
            }
            else
            {
                var process = Run(installation.InstallationPath, "Aurora.exe");
                processes.Insert(0, process);
            }

            return processes;
        }

        private static void CopyToFolder(Mod mod, string folder)
        {
            var dir = Path.GetDirectoryName(mod.Installation.ModFolder);
            foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).Where(f => !Path.GetFileName(f).Equals("mod.ini")))
            {
                File.Copy(file, Path.Combine(folder, Path.GetFileName(file)), true);
            }
        }

        private static Process Run(string folder, string command)
        {
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
            var info = new ProcessStartInfo()
            {
                WorkingDirectory = folder,
                FileName = Path.Combine(folder, exe),
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            //info.EnvironmentVariables["PATH"] = java + ";" + Environment.GetEnvironmentVariable("PATH");

            try
            {
                var process = Process.Start(info);
                return process;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to run {command}", e);
                return null;
            }

        }

        private static void InstallThemeMod(Mod mod, GameInstallation installation)
        {
            Program.CopyDirectory(mod.Installation.ModFolder, installation.InstallationPath);
            Log.Debug($"Installed theme mod: {mod.Name}");
        }

        private static void UninstallThemeMods(GameInstallation installation, ModRegistry registry)
        {
            var installed = registry.Mods.Where(m => m.Installed && m.Type == ModType.THEME).ToList();

            foreach (var mod in installed)
            {
                foreach (var file in Directory.EnumerateFiles(mod.Installation.ModFolder, "*.*", SearchOption.AllDirectories))
                {
                    var out_file = Path.Combine(installation.InstallationPath, Path.GetRelativePath(mod.Installation.ModFolder, file));
                    if (File.Exists(out_file))
                    {
                        File.Delete(out_file);
                    }
                }

                Log.Debug($"Uninstalled theme mod: {mod.Name}");
            }
        }

        private static void InstallDbMod(Mod mod, GameInstallation installation)
        {
            const string TABLE = "CREATE TABLE IF NOT EXISTS A_THIS_SAVE_IS_MODDED (ModName Text PRIMARY KEY);";

            using (var connection = new SQLiteConnection(GetConnectionString(installation)))
            {
                connection.Open();
                var table = new SQLiteCommand(TABLE, connection);
                table.ExecuteNonQuery();

                var files = Directory.EnumerateFiles(mod.Installation.ModFolder, "*.sql").ToList();
                try
                {
                    var uninstall = files.Single(f => Path.GetFileName(f).Equals("uninstall.sql"));
                    files.Remove(uninstall);
                }
                catch (Exception)
                {
                    Log.Debug($"No uninstall for db mod: {mod.Name}");
                }

                foreach (var file in files)
                {
                    var sql = File.ReadAllText(file);
                    var command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();

                    sql = $"INSERT INTO A_THIS_SAVE_IS_MODDED(ModName)" +
                            $"SELECT '{mod.Name}'" +
                            $"WHERE NOT EXISTS(SELECT 1 FROM A_THIS_SAVE_IS_MODDED WHERE ModName = '{mod.Name}');";

                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();

                    Log.Debug($"Installed db mod: {mod.Name}");
                }

                connection.Close();
            }
        }

        private static void UninstallDbMods(GameInstallation installation, ModRegistry registry)
        {
            var installed = registry.Mods.Where(m => m.Installed && m.Type == ModType.DATABASE).ToList();

            using (var connection = new SQLiteConnection(GetConnectionString(installation)))
            {
                connection.Open();

                var sql = "SELECT * FROM sqlite_master WHERE name ='A_THIS_SAVE_IS_MODDED' and type='table';";
                var command = new SQLiteCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return;
                }
                else
                {
                    reader.Close();
                }

                sql = "SELECT * FROM A_THIS_SAVE_IS_MODDED";
                command = new SQLiteCommand(sql, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader[0].ToString();
                    var mod = installed.FirstOrDefault(m => m.Name.Equals(name));

                    if (mod == null)
                    {
                        throw new Exception($"Installed db mod {name} not found");
                    }
                    else
                    {
                        var file = Path.Combine(mod.Installation.ModFolder, "uninstall.sql");
                        if (!File.Exists(file))
                        {
                            throw new Exception($"Db mod {name} uninstall.sql not found");
                        }

                        sql = File.ReadAllText(file);
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
    }
}
