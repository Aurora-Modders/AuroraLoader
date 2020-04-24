using Semver;
using System;

namespace AuroraLoader
{
    public class GameVersion : IComparable<GameVersion>
    {
        public SemVersion Version { get; }
        public string Checksum { get; }

        public GameVersion(string version, string checksum)
        {
            if (version[0] == 65279)
            {
                version = version.Substring(1);
            }

            Version = version;
            Checksum = checksum;
        }

        public int CompareTo(GameVersion other)
        {
            return Version.CompareByPrecedence(other.Version);
        }
    }
}
