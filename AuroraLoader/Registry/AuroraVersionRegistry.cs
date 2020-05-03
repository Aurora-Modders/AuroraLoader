using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly string _versionCachePath = Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora_versions.ini");

        public AuroraVersionRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Update(IList<string> mirrors = null)
        {
            if (!File.Exists(_versionCachePath) && mirrors == null)
            {
                throw new Exception($"Aurora version cache not found at {_versionCachePath} and no mirrors provided");
            }
            else if (File.Exists(_versionCachePath) && mirrors == null)
            {
                UpdateKnownVersionsFromCache();
            }
            else if (mirrors != null)
            {
                UpdateKnownAuroraVersionsFromMirrors(mirrors);
            }

            var checksum = GetChecksum(File.ReadAllBytes(Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora.exe")));
            Log.Debug($"Identified checksum {checksum}");
            try
            {
                CurrentAuroraVersion = AuroraVersions.Single(v => v.Checksum.Equals(checksum));
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't find Aurora version associated with checksum {checksum}", e);
                CurrentAuroraVersion = new AuroraVersion(SemVersion.Parse("1.0.0"), checksum);
            }
            Log.Debug($"Running Aurora {CurrentAuroraVersion.Version}");
        }

        internal void UpdateKnownVersionsFromCache()
        {
            Log.Debug($"Loading Aurora versions from {Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora_versions.ini")}");
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

        internal void UpdateKnownAuroraVersionsFromMirrors(IList<string> mirrors)
        {
            var allKnownVersions = new List<AuroraVersion>(AuroraVersions);
            foreach (var mirror in mirrors)
            {
                Log.Debug($"Retrieving version information from {mirror} if available");
                var mirrorKnownVersions = new List<AuroraVersion>();
                var versionsUrl = Path.Combine(mirror, "aurora_versions.ini");
                using (var client = new WebClient())
                {
                    try
                    {
                        var response = client.DownloadString(versionsUrl);
                        mirrorKnownVersions.AddRange(ModConfigurationReader.AuroraVersionsFromString(response));
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Didn't find an Aurora version listing at {versionsUrl}", e);
                    }
                }
                foreach (var version in mirrorKnownVersions)
                {
                    if (!allKnownVersions.Any(existing => version.Checksum == existing.Checksum))
                    {
                        allKnownVersions.Add(version);
                    }
                }
            }
            AuroraVersions = allKnownVersions;

            UpdateCache();
        }

        internal void UpdateCache()
        {
            // Update cache
            var versions = AuroraVersions.Select(v => $"{v.Version}={v.Checksum}");

            Log.Debug($"Updating cache with {String.Join("\n\r", versions)}");
            File.WriteAllLines(
                Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora_versions.ini"),
                AuroraVersions.Select(v => $"{v.Version}={v.Checksum}"));
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
