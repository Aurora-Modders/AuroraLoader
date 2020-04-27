using Semver;

namespace AuroraLoader.Mods
{
    public class ModConfiguration
    {
        public string Name { get; internal set; } = null;
        public ModType Type { get; internal set; } = ModType.EXE;
        public ModStatus Status { get; internal set; } = ModStatus.POWERUSER;

        public SemVersion Version { get; internal set; } = null;

        public SemVersion HighestInstalledVersion { get; internal set; } = null;

        public ModCompabitilityVersion TargetAuroraVersion { get; internal set; }
        public string ExecuteCommand { get; internal set; } = null;
        public string ModInternalConfigFile { get; internal set; } = null;
        public string Updates { get; internal set; } = null;
        public string ModFolder { get; }

        internal ModConfiguration(string ModFolder)
        {
            this.ModFolder = ModFolder;
        }

        public bool WorksForVersion(AuroraVersion version)
        {
            // TODO Needs to understand wildcarded versions
            return TargetAuroraVersion.WorksForVersion(version.Version);
        }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }
    }
}
