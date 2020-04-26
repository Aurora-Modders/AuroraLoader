using Semver;
using System;

namespace AuroraLoader
{
    public class AuroraVersion : IComparable<AuroraVersion>
    {
        public SemVersion Version { get; }
        public string Checksum { get; }

        public AuroraVersion(SemVersion version, string checksum)
        {
            Version = version;
            Checksum = checksum;
        }

        public int CompareTo(AuroraVersion other)
        {
            return Version.CompareByPrecedence(other.Version);
        }
    }
}
