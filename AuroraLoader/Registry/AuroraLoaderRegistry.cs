using Microsoft.Extensions.Configuration;
using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AuroraLoader
{
    public class AuroraLoaderRegistry
    {
        public IList<Mirror> Mirrors { get; private set; }
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

        public AuroraLoaderRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
            UpdateLocallyKnownMirrors();
            UpdateLocalMods();
            UpdateKnownVersions();
        }

        public void UpdateLocallyKnownMirrors()
        {
            var mirrors = new List<Mirror>();
            try
            {
                foreach (var rootUrl in File.ReadAllLines(_configuration["aurora_mirrors_relative_filepath"]))
                {
                    mirrors.Add(new Mirror(_configuration, rootUrl));
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse mirror data from {_configuration["aurora_mirrors_relative_filepath"]}", e);
            }

            Mirrors = mirrors;
        }

        public void UpdateLocalMods()
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
            AuroraVersions = UpdateLocallyKnownVersions().Union(UpdateMirrorKnownVersions()).ToList();
        }

        private IList<AuroraVersion> UpdateLocallyKnownVersions()
        {
            var knownVersions = new List<AuroraVersion>();
            try
            {
                foreach (var kvp in Config.FromString(File.ReadAllText(_configuration["aurora_known_versions_relative_filepath"])))
                {
                    knownVersions.Add(new AuroraVersion(SemVersion.Parse(kvp.Key), kvp.Value));
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse version data from {_configuration["aurora_known_versions_relative_filepath"]}", e);
            }
            return knownVersions;
        }

        private IList<AuroraVersion> UpdateMirrorKnownVersions()
        {
            var mirrorKnownVersions = new List<AuroraVersion>();
            foreach (var mirror in Mirrors)
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
            return mirrorKnownVersions;
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
