using System;
using AuroraLoader.Mods;
using Semver;

namespace AuroraLoader
{
    public class AuroraVersion : IComparable<AuroraVersion>
    {
        public SemVersion Version { get; }
        public string Checksum { get; }

        public AuroraVersion(SemVersion version, string checksum)
        {
            if (version == null || checksum == null)
            {
                throw new ArgumentNullException();
            }

            Version = version;
            Checksum = checksum;
        }

        public int CompareTo(AuroraVersion other)
        {
            return Version.CompareByPrecedence(other.Version);
        }

        public bool CompatibleWith(ModCompabitilityVersion modCompatibility)
        {
            if (modCompatibility.Major != -1 && Version.Major != modCompatibility.Major)
            {
                return false;
            }
            else if (modCompatibility.Minor != -1 && Version.Minor != modCompatibility.Minor)
            {
                return false;
            }
            else if (modCompatibility.Patch != -1 && Version.Patch != modCompatibility.Patch)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
