using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AuroraLoader
{
    public class LocalModRegistry
    {
        public IList<Mod> LocalMods { get; private set; }
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

        public LocalModRegistry(IConfiguration configuration, MirrorRegistry mirrorRegistry)
        {
            _configuration = configuration;
            _mirrorRegistry = mirrorRegistry;
            UpdateKnownVersions();
        }

        public void UpdateDownloadedMods()
        {
            var mods = new List<Mod>();
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
            {
                try
                {
                    mods.Add(Mod.Parse(file));
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to parse mod data from {file}", e);
                }
            }
            LocalMods = mods;
        }

        public void UpdateKnownVersions()
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

        public void UpdateKnownAuroraVersionsFromMirror()
        {
            UpdateKnownVersions();
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
