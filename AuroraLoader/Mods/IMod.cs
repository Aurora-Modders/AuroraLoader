using System.Diagnostics;
using Semver;

namespace AuroraLoader.Mods
{
    public interface IModVersion
    {
        public SemVersion Version { get; }
        public ModCompabitilityVersion TargetAuroraVersion { get; }
        public string DownloadUrl { get; }
        public string DownloadPath { get; }
        public bool Downloaded { get; }
        public Mod Mod { get; set; }


        public void Download();
        public void Install(AuroraInstallation installation);
        public void Uninstall(AuroraInstallation installation);
        public Process Run();
    }
}
