using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AuroraLoader
{
    public class GameVersion : IComparable<GameVersion>
    {
        public static List<GameVersion> GetKnownGameVersions()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods", "aurora_versions.txt");
            var known = new Dictionary<SemVersion, string>();
            foreach (var kvp in Config.FromString(File.ReadAllText(file)))
            {
                known[SemVersion.Parse(kvp.Key)] = kvp.Value;
            }

            try
            {
                using (var client = new WebClient())
                {
                    foreach (var mirror in Program.MIRRORS)
                    {
                        var versions = client.DownloadString(mirror + "Aurora/aurora_versions.txt");
                        foreach (var kvp in Config.FromString(versions))
                        {
                            known[SemVersion.Parse(kvp.Key)] = kvp.Value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed to get known Aurora versions.", e);
            }

            return known.Select(kvp => new GameVersion(kvp.Key.ToString(), kvp.Value)).ToList();
        }

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
