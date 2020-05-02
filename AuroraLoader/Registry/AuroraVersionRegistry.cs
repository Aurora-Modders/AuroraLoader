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

        public AuroraVersionRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Update(IList<string> mirrors = null)
        {
            UpdateKnownVersionsFromCache();
            if (mirrors != null)
            {
                UpdateKnownAuroraVersionsFromMirrors(mirrors);
                // Update cache
                File.WriteAllLines(
                    Path.Combine(Program.AuroraLoaderExecutableDirectory, "aurora_versions.ini"),
                    AuroraVersions.Select(v => $"{v.Version}={v.Checksum}"));
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

        internal void UpdateKnownAuroraVersionsFromMirrors(IList<string> mirrors)
        {
            var allKnownVersions = new List<AuroraVersion>(AuroraVersions);
            foreach (var mirror in mirrors)
            {
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
                        throw;
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
