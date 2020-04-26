using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AuroraLoader.Mods;

namespace AuroraLoader
{
    static class Launcher
    {
        public static Process Launch(IList<Mod> mods, Mod executableMod = null)
        {
            if (mods.Any(mod => mod.Type == ModType.EXE))
            {
                throw new Exception("Use the other parameter");
            }

            foreach (var mod in mods.Where(mod => mod.Type == ModType.ROOTUTILITY))
            {
                Log.Debug("Root Utility: " + mod.Name);
                CopyToRoot(mod);
                Run(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), mod.Installation.ExecuteCommand);
            }
            foreach (var mod in mods.Where(mod => mod.Type == ModType.UTILITY))
            {
                Log.Debug("Utility: " + mod.Name);
                Run(Path.GetDirectoryName(mod.Installation.ModFolder), mod.Installation.ExecuteCommand);
            }
            foreach (var mod in mods.Where(mod => mod.Type == ModType.DATABASE))
            {
                Log.Debug("Database: " + mod.Name);
                throw new Exception("Database mods not supported yet: " + mod.Name);
            }

            if (executableMod != null)
            {
                CopyToRoot(executableMod);
                return Run(Program.AuroraLoaderExecutableDirectory, executableMod.Installation.ExecuteCommand);
            }
            else
            {
                return Run(Program.AuroraLoaderExecutableDirectory, "Aurora.exe");
            }
        }

        private static void CopyToRoot(Mod mod)
        {
            var dir = Path.GetDirectoryName(mod.Installation.ModFolder);
            foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).Where(f => !Path.GetFileName(f).Equals("mod.ini")))
            {
                File.Copy(file, Path.Combine(Program.AuroraLoaderExecutableDirectory, Path.GetFileName(file)), true);
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
    }
}
