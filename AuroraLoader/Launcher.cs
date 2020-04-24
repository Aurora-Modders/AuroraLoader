using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AuroraLoader
{
    static class Launcher
    {
        public static Process Launch(Mod exe, List<Mod> others)
        {
            foreach (var mod in others)
            {
                if (mod.Type == Mod.ModType.ROOT_UTILITY)
                {
                    Debug.WriteLine("Root Utility: " + mod.Name);
                    CopyToRoot(mod);
                    Run(AppDomain.CurrentDomain.BaseDirectory, mod.Exe);
                }
                else if (mod.Type == Mod.ModType.UTILITY)
                {
                    Debug.WriteLine("Utility: " + mod.Name);
                    Run(Path.GetDirectoryName(mod.DefFile), mod.Exe);
                }
                else if (mod.Type == Mod.ModType.DATABASE)
                {
                    Debug.WriteLine("Database: " + mod.Name);
                    throw new Exception("Database mods not supported yet: " + mod.Name);
                }
                else
                {
                    throw new Exception("Invalid mod: " + mod.Name);
                }
            }

            if (exe.Name.Equals("Base Game"))
            {
                Debug.WriteLine("Exe: " + exe.Name);
                var process = Run(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe");

                return process;
            }
            else
            {
                Debug.WriteLine("Exe: " + exe.Name);
                CopyToRoot(exe);
                var process = Run(AppDomain.CurrentDomain.BaseDirectory, exe.Exe);

                return process;
            }
        }

        private static void CopyToRoot(Mod mod)
        {
            var dir = Path.GetDirectoryName(mod.DefFile);
            var out_dir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).Where(f => !Path.GetFileName(f).Equals("mod.ini")))
            {
                File.Copy(file, Path.Combine(out_dir, Path.GetFileName(file)), true);
            }
        }

        private static Process Run(string folder, string command)
        {
            //var java = Environment.GetEnvironmentVariable("PROGRAMFILES(X86)") + @"\Common Files\Oracle\Java\javapath";

            Debug.WriteLine("Running: " + command);
            var info = new ProcessStartInfo()
            {
                WorkingDirectory = folder,
                FileName = command, 
                UseShellExecute = true,
                CreateNoWindow = true
            };
            //info.EnvironmentVariables["PATH"] = java + ";" + Environment.GetEnvironmentVariable("PATH");
            
            var process = Process.Start(info);
            return process;
        }
    }
}
