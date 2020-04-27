using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AuroraLoader.Mods;

namespace AuroraLoader
{
    static class Launcher
    {
        private const string CONNECTION_STRING = "Data Source=AuroraDB.db;Version=3;New=False;Compress=True;";

        public static List<Process> Launch(GameInstallation installation, IList<Mod> mods, Mod executableMod = null)
        {
            if (mods.Any(mod => mod.Type == ModType.EXE))
            {
                throw new Exception("Use the other parameter");
            }

            var processes = new List<Process>();

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
            foreach (var mod in mods.Where(mod => mod.Type == ModType.DATABASE))
            {
                Log.Debug("Database: " + mod.Name);
                ApplyDbMod(mod, installation);
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
                FileName = exe,
                Arguments = args,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            //info.EnvironmentVariables["PATH"] = java + ";" + Environment.GetEnvironmentVariable("PATH");

            var process = Process.Start(info);
            return process;
        }

        private static void ApplyDbMod(Mod mod, GameInstallation installation)
        {
            using (var connection = new SQLiteConnection(CONNECTION_STRING))
            {
                connection.Open();

                foreach (var file in Directory.EnumerateFiles(mod.Installation.ModFolder, "*.sql"))
                {
                    var sql = File.ReadAllText(file);
                    var command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
