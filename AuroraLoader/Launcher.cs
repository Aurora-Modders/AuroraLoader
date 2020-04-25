﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AuroraLoader.Registry;

namespace AuroraLoader
{
    static class Launcher
    {
        public static Process Launch(ModInstallation exe, List<ModInstallation> others)
        {
            // TODO change signature to string executablePath and put this logic elsewhere
            foreach (var mod in others)
            {
                if (mod.Type == ModType.ROOT_UTILITY)
                {
                    Log.Debug("Root Utility: " + mod.Name);
                    CopyToRoot(mod);
                    Run(AppDomain.CurrentDomain.BaseDirectory, mod.ExecuteCommand);
                }
                else if (mod.Type == ModType.UTILITY)
                {
                    Log.Debug("Utility: " + mod.Name);
                    Run(Path.GetDirectoryName(mod.ModFolder), mod.ExecuteCommand);
                }
                else if (mod.Type == ModType.DATABASE)
                {
                    Log.Debug("Database: " + mod.Name);
                    throw new Exception("Database mods not supported yet: " + mod.Name);
                }
                else
                {
                    throw new Exception("Invalid mod: " + mod.Name);
                }
            }

            var process = Run(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe");

            return process;

            //if (exe.Name.Equals("Base Game"))
            //{
            //    Log.Debug("Exe: " + exe.Name);
            //    var process = Run(AppDomain.CurrentDomain.BaseDirectory, "Aurora.exe");

            //    return process;
            //}
            //else
            //{
            //    Log.Debug("Exe: " + exe.Name);
            //    CopyToRoot(exe);
            //    var process = Run(AppDomain.CurrentDomain.BaseDirectory, exe.Exe);

            //    return process;
            //}
        }

        private static void CopyToRoot(ModInstallation mod)
        {
            var dir = Path.GetDirectoryName(mod.ModFolder);
            var out_dir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).Where(f => !Path.GetFileName(f).Equals("mod.ini")))
            {
                File.Copy(file, Path.Combine(out_dir, Path.GetFileName(file)), true);
            }
        }

        private static Process Run(string folder, string command)
        {
            //var java = Environment.GetEnvironmentVariable("PROGRAMFILES(X86)") + @"\Common Files\Oracle\Java\javapath";

            Log.Debug("Running: " + command);
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
