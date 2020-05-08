using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using AuroraLoader.Mods;
using AuroraLoader.Registry;
using Semver;
[assembly: InternalsVisibleTo("AuroraLoaderTest")]

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
            if (version == null || installationPath == null)
            {
                throw new ArgumentNullException();
            }

            InstalledVersion = version;
            InstallationPath = installationPath;
        }

        public List<Process> Launch(IList<ModVersion> modVersions, ModVersion executableMod = null)
        {
            Log.Debug($"Launching from {InstallationPath}");
            var processes = new List<Process>();

            foreach (var mod in modVersions.Where(v => v.Mod.Type == ModType.DATABASE || v.Mod.Type == ModType.THEME))
            {
                try
                {
                    mod.Install(this);
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to launch {mod.Mod.Name}", e);
                }
            }

            foreach (var modVersion in modVersions.Where(v => v.Mod.Type == ModType.ROOTUTILITY || v.Mod.Type == ModType.UTILITY))
            {
                try
                {
                    modVersion.Install(this);
                    processes.Add(modVersion.Run(this));
                }
                catch (Exception e)
                {
                    var message = $"Failed to launch {modVersion.Mod.Name} {modVersion.Version}";
                    Log.Error(message, e);
                }
            }

            if (executableMod != null)
            {
                executableMod.Uninstall(this);
                executableMod.Install(this);
                var process = executableMod.Run(this);
                processes.Insert(0, process);
            }
            else
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = InstallationPath,
                    FileName = "Aurora.exe",
                    UseShellExecute = true,
                    CreateNoWindow = true
                };
                processes.Insert(0, Process.Start(processStartInfo));
            }

            return processes;
        }

        public void Cleanup(IList<ModVersion> modVersions)
        {
            foreach (var mod in modVersions.Where(v => v.Mod.Type == ModType.DATABASE || v.Mod.Type == ModType.THEME))
            {
                try
                {
                    mod.Uninstall(this);
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to uninstall {mod.Mod.Name}", e);
                }
            }
        }

        public void UpdateAurora(Dictionary<string, string> aurora_files)
        {
            if (aurora_files == null)
            {
                throw new ArgumentNullException();
            }
            if (!aurora_files.Any())
            {
                throw new ArgumentException("aurora_files");
            }

            var update = SemVersion.Parse(aurora_files["Version"]);

            if (InstalledVersion.Version.Major == update.Major)
            {
                aurora_files.Remove("Major");

                if (InstalledVersion.Version.Minor == update.Minor)
                {
                    aurora_files.Remove("Minor");

                    if (InstalledVersion.Version.Patch == update.Patch)
                    {
                        aurora_files.Remove("Patch");
                        aurora_files.Remove("Rev"); // deprecated
                    }
                }
            }

            foreach (var piece in aurora_files.Keys.ToList())
            {
                if (!piece.Equals("Major") && !piece.Equals("Minor") && !piece.Equals("Patch") && !piece.Equals("Rev"))
                {
                    aurora_files.Remove(piece);
                }
            }

            if (aurora_files.Count > 0)
            {
                Installer.DownloadAuroraPieces(InstallationPath, aurora_files);
            }
        }
    }
}