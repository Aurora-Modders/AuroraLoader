using System.IO;

namespace AuroraLoader
{
    public class GameInstallation
    {
        public readonly AuroraVersion InstalledVersion;
        public readonly string InstallationPath;

        // e.g. <install dir>/Aurora/1.8.0
        public string VersionedDirectory => Path.Combine(InstallationPath, "Aurora", InstalledVersion.Version.ToString());

        public GameInstallation(AuroraVersion version, string installationPath)
        {
            InstalledVersion = version;
            InstallationPath = installationPath;

        }
    }
}