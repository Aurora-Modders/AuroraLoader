namespace AuroraLoader
{
    public class GameInstallation
    {
        public readonly AuroraVersion InstalledVersion;
        public readonly string InstallationPath;

        public GameInstallation(AuroraVersion version, string installationPath)
        {
            InstalledVersion = version;
            InstallationPath = installationPath;

        }
    }
}