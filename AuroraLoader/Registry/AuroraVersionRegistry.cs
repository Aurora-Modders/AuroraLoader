using Microsoft.Extensions.Configuration;
using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class AuroraVersionRegistry : IRegistry
    {
        public IList<AuroraVersion> AuroraVersions { get; private set; }
        public AuroraVersion CurrentAuroraInstallVersion
        {
            get
            {
                var checksum = GetChecksum(File.ReadAllBytes(_configuration["executable_location"]));
                return AuroraVersions.Single(v => v.Checksum.Equals(checksum));
            }
        }

        private readonly IConfiguration _configuration;
        private readonly MirrorRegistry _mirrorRegistry;

        public AuroraVersionRegistry(IConfiguration configuration, MirrorRegistry mirrorRegistry)
        {
            _configuration = configuration;
            _mirrorRegistry = mirrorRegistry;
        }

        public void Update()
        {
            _mirrorRegistry.Update();
            UpdateKnownAuroraVersionsFromMirror();
            if (!AuroraVersions.Any())
            {
                UpdateKnownVersionsFromCache();
            }
        }

        internal void UpdateKnownAuroraVersionsFromMirror()
        {
            var mirrorKnownVersions = new List<AuroraVersion>();
            foreach (var mirror in _mirrorRegistry.Mirrors)
            {
                try
                {
                    mirror.UpdateKnownAuroraVersions();
                    mirrorKnownVersions = mirrorKnownVersions.Union(mirror.KnownAuroraVersions).ToList();
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to update known Aurora versions from {mirror.RootUrl}", e);
                }
            }

            AuroraVersions = mirrorKnownVersions;
            // TODO update local cache
        }

        internal void UpdateKnownVersionsFromCache()
        {
            var auroraVersions = new List<AuroraVersion>();
            try
            {
                foreach (var kvp in Config.FromString(File.ReadAllText(_configuration["aurora_known_versions_relative_filepath"])))
                {
                    auroraVersions.Add(new AuroraVersion(SemVersion.Parse(kvp.Key), kvp.Value));
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse version data from {_configuration["aurora_known_versions_relative_filepath"]}", e);
            }
            AuroraVersions = auroraVersions;
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
