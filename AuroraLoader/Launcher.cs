using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AuroraLoader
{
    static class Launcher
    {
        public static Process Launch(Mod exe, List<Mod> others, string folder)
        {
            foreach (var mod in others)
            {
                if (mod.Type == Mod.ModType.ROOT_UTILITY)
                {
                    Log.Debug("Root Utility: " + mod.Name);
                    CopyToFolder(mod, folder);
                    Run(AppDomain.CurrentDomain.BaseDirectory, mod.Exe);
                }
                else if (mod.Type == Mod.ModType.UTILITY)
                {
                    Log.Debug("Utility: " + mod.Name);
                    Run(Path.GetDirectoryName(mod.DefFile), mod.Exe);
                }
                else if (mod.Type == Mod.ModType.DATABASE)
                {
                    Log.Debug("Database: " + mod.Name);
                    throw new Exception("Database mods not supported yet: " + mod.Name);
                }
                else
                {
                    throw new Exception("Invalid mod: " + mod.Name);
                }
            }

            if (exe.Name.Equals("Base Game"))
            {
                Log.Debug("Exe: " + exe.Name);
                var process = Run(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe");

                return process;
            }
            else
            {
                Log.Debug("Exe: " + exe.Name);
                CopyToFolder(exe, folder);
                var process = Run(AppDomain.CurrentDomain.BaseDirectory, exe.Exe);

                return process;
            }
        }

        private static void CopyToFolder(Mod mod, string folder)
        {
            var dir = Path.GetDirectoryName(mod.DefFile);
            Program.CopyDirectory(dir, folder);
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
