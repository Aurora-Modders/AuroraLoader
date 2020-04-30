using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AuroraLoader.Mods;
using Microsoft.Extensions.Configuration;
using Semver;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class AuroraVersionRegistry
    {
        public IList<AuroraVersion> AuroraVersions { get; private set; } = new List<AuroraVersion>();

        public AuroraVersion CurrentAuroraVersion { get; private set; }

        private readonly IConfiguration _configuration;

        public AuroraVersionRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Update(IList<Mirror> mirrors = null)
        {
            UpdateKnownVersionsFromCache();
            if (mirrors != null)
            {
                UpdateKnownAuroraVersionsFromMirrors(mirrors);
            }

            var checksum = GetChecksum(File.ReadAllBytes(Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora.exe")));
            try
            {
                CurrentAuroraVersion = AuroraVersions.Single(v => v.Checksum.Equals(checksum));
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't find Aurora version associated with checksum {checksum}", e);
                CurrentAuroraVersion = new AuroraVersion(SemVersion.Parse("1.0.0"), checksum);
            }
        }

        internal void UpdateKnownVersionsFromCache()
        {
            try
            {
                var rawFileContents = File.ReadAllText(Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora_versions.ini"));
                AuroraVersions = ModConfigurationReader.AuroraVersionsFromString(rawFileContents).ToList();
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse version data from {_configuration["aurora_known_versions_relative_filepath"]}", e);
            }
        }

        internal void UpdateKnownAuroraVersionsFromMirrors(IList<Mirror> mirrors)
        {
            var mirrorKnownVersions = new List<AuroraVersion>(AuroraVersions);
            foreach (var mirror in mirrors)
            {
                mirror.UpdateKnownAuroraVersions();
                try
                {
                    mirror.UpdateKnownAuroraVersions();
                    foreach (var version in mirror.KnownAuroraVersions)
                    {
                        if (!mirrorKnownVersions.Any(existing => version.Checksum == existing.Checksum))
                        {
                            mirrorKnownVersions.Add(version);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to update known Aurora versions from {mirror.RootUrl}", e);
                }
            }

            AuroraVersions = mirrorKnownVersions.Union(AuroraVersions).ToList();
        }

        internal string GetChecksum(byte[] bytes)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(bytes);
            var str = Convert.ToBase64String(hash);

            return str.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 6);
        }
    }
}
