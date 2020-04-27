using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AuroraLoader.Mods;
using Microsoft.Extensions.Configuration;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class AuroraVersionRegistry : IRegistry
    {
        public IList<AuroraVersion> AuroraVersions { get; private set; } = new List<AuroraVersion>();

        public AuroraVersion CurrentAuroraVersion
        {
            get
            {
                try
                {
                    var checksum = GetChecksum(File.ReadAllBytes(Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora.exe")));
                    return AuroraVersions.Single(v => v.Checksum.Equals(checksum));
                }
                catch
                {
                    return null;
                }

            }
        }

        private readonly IConfiguration _configuration;
        private readonly MirrorRegistry _mirrorRegistry;

        public AuroraVersionRegistry(IConfiguration configuration, MirrorRegistry mirrorRegistry)
        {
            _configuration = configuration;
            _mirrorRegistry = mirrorRegistry;
        }

        public void Update(AuroraVersion version)
        {
            _mirrorRegistry.Update(version);
            UpdateKnownVersionsFromCache();
            UpdateKnownAuroraVersionsFromMirror();
        }

        internal void UpdateKnownAuroraVersionsFromMirror()
        {
            var mirrorKnownVersions = new List<AuroraVersion>(AuroraVersions);
            foreach (var mirror in _mirrorRegistry.Mirrors)
            {
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
            // TODO update local cache
        }

        internal void UpdateKnownVersionsFromCache()
        {
            try
            {
                var rawFileContents = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "aurora_versions.ini"));
                AuroraVersions = ModConfigurationReader.AuroraVersionsFromString(rawFileContents).ToList();
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse version data from {_configuration["aurora_known_versions_relative_filepath"]}", e);
            }
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
