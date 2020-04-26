using Semver;
using System;

namespace AuroraLoader
{
    public class AuroraInstallation : IComparable<AuroraInstallation>
    {
        public SemVersion Version { get; }
        public string Checksum { get; }

        public AuroraInstallation(SemVersion version, string checksum)
        {
            Version = version;
            Checksum = checksum;
        }

        public int CompareTo(AuroraInstallation other)
        {
            return Version.CompareByPrecedence(other.Version);
        }
    }
}
