using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AuroraLoader.Mods;

namespace AuroraLoader
{
    public class AuroraInstallation
    {
        public readonly AuroraVersion InstalledVersion;
        public readonly string InstallationPath;
        public string ConnectionString => $"Data Source={Path.Combine(InstallationPath, "AuroraDB.db")};Version=3;New=False;Compress=True;";

        // e.g. <install dir>/Aurora/1.8.0
        public string VersionedDirectory => Path.Combine(InstallationPath, "Aurora", InstalledVersion.Version.ToString());

        public AuroraInstallation(AuroraVersion version, string installationPath)
        {
            InstalledVersion = version;
            InstallationPath = installationPath;
        }

        public List<Process> Launch(IList<Mod> enabledMods, Mod executableMod = null)
        {
            Log.Debug($"Launching from {InstallationPath}");
            if (enabledMods.Any(mod => mod.Type == ModType.EXECUTABLE))
            {
                throw new Exception("Use the other parameter");
            }

            var processes = new List<Process>();

            // Install selected mods (including executable, if provided)
            foreach (var mod in enabledMods.Union(new List<Mod>() { executableMod }))
            {
                if (mod.LatestInstalledVersionCompatibleWith(InstalledVersion) == null)
                {
                    throw new Exception($"{mod.Name} selected for use with {InstalledVersion} but is only compatible with {mod.LatestInstalledVersion.TargetAuroraVersion}");
                }

                foreach (var version in mod.Downloads.Where(d => d.Downloaded))
                {
                    version.Uninstall(this);
                }

                mod.LatestInstalledVersionCompatibleWith(InstalledVersion).Install(this);
            }

            foreach (var mod in enabledMods.Where(mod => mod.Type == ModType.ROOTUTILITY || mod.Type == ModType.UTILITY))
            {
                var process = mod.LatestInstalledVersionCompatibleWith(InstalledVersion).Run();
                processes.Add(process);
            }

            if (executableMod != null)
            {
                processes.Insert(0, executableMod.LatestInstalledVersionCompatibleWith(InstalledVersion).Run());
            }
            else
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = InstallationPath,
                    FileName = "Aurora.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                processes.Insert(0, Process.Start(processStartInfo));
            }

            return processes;
        }
    }
}