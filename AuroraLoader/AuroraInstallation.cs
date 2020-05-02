using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
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

        public Process Launch(IList<ModVersion> modVersions, ModVersion executableMod = null)
        {
            Log.Debug($"Launching from {InstallationPath}");
            if (selectedModVersions.Any(mv => mv.Mod.Type == ModType.EXECUTABLE))
            {
                throw new Exception("Use the executableMod parameter");
            }

            var processes = new List<Process>();

            // Install selected mods (including executable, if provided)
            foreach (var modVersion in modVersions)
            {
                modVersion.Uninstall(this);
                modVersion.Install(this);
                var process = modVersion.Run();
                if (process != null)
                {
                    processes.Add(process);
                }
                else
                {
                    MessageBox.Show($"Failed to launch {modVersion.Mod.Name} {modVersion.Version}");
                }
            }

            if (executableMod != null)
            {
                executableMod.Uninstall(this);
                executableMod.Install(this);
                var process = executableMod.Run();
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