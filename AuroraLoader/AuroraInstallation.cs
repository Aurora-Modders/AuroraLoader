using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AuroraLoader.Mods;
using Semver;
[assembly: InternalsVisibleTo("AuroraLoaderTest")]

namespace AuroraLoader
{
    public class AuroraInstallation
    {
        public readonly AuroraVersion InstalledVersion;
        public readonly string InstallationPath;
        public string ConnectionString => $"Data Source={Path.Combine(InstallationPath, "AuroraDB.db")};Version=3;New=False;Compress=True;";

        internal readonly IList<ModVersion> selectedModVersions = new List<ModVersion>();

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

            if (!Directory.Exists(VersionedDirectory))
            {
                Directory.CreateDirectory(VersionedDirectory);
            }
        }

        public void SelectModVersion(ModVersion modVersion)
        {
            if (modVersion == null)
            {
                // TODO should really use reflection for this
                throw new ArgumentNullException("modVersion");
            }

            if (selectedModVersions.Any(mv => mv.Mod.Name == modVersion.Mod.Name))
            {
                throw new ArgumentException($"{modVersion.Mod.Name} already selected");
            }
            selectedModVersions.Add(modVersion);
        }

        public void DeselectModVersion(ModVersion modVersion)
        {
            if (modVersion == null)
            {
                throw new ArgumentNullException("modVersion");
            }

            if (!selectedModVersions.Remove(modVersion))
            {
                // Just to smoke out UI issues - we'll catch it on the other end
                throw new ArgumentException($"{modVersion.Mod.Name} not selected");
            }
        }

        public Process Launch()
        {
            Log.Debug($"Launching from {InstallationPath}");
            if (selectedModVersions.Count(mv => mv.Mod.Type == ModType.EXECUTABLE) > 1)
            {
                throw new Exception("More than one executable mod selected");
            }

            var processes = new List<Process>();

            // Install selected mods (including executable, if provided)
            foreach (var modVersion in selectedModVersions)
            {
                modVersion.Uninstall(this);
                modVersion.Install(this);
            }

            foreach (var modVersion in selectedModVersions.Where(modVersion => modVersion.Mod.Type == ModType.ROOTUTILITY || modVersion.Mod.Type == ModType.UTILITY))
            {
                var process = modVersion.Run();
                processes.Add(process);
            }

            var executableMod = selectedModVersions.SingleOrDefault(mv => mv.Mod.Type == ModType.EXECUTABLE);
            if (executableMod != null)
            {
                processes.Insert(0, executableMod.Run());
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

            return processes[0];
        }

        public void CreateBackup()
        {
            Directory.CreateDirectory(VersionedDirectory);
            File.Copy(Path.Combine(InstallationPath, "Aurora.exe"), Path.Combine(VersionedDirectory, "Aurora.exe"), true);
            File.Copy(Path.Combine(InstallationPath, "AuroraDB.db"), Path.Combine(VersionedDirectory, "AuroraDB.db"), true);
        }

        public void UpdateAurora(Dictionary<string, string> aurora_files)
        {
            if (aurora_files == null)
            {
                throw new ArgumentNullException();
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