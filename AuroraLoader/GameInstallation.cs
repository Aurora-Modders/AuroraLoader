namespace AuroraLoader
{
    public class GameInstallation
    {
        public readonly AuroraVersion InstalledVersion;
        public readonly string ExecutableLocation;

        public GameInstallation(AuroraVersion version, string executable)
        {
            InstalledVersion = version;
            ExecutableLocation = executable;

        }
    }
}