using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AuroraLoader
{
    public class GameInstallation
    {
        public GameVersion InstalledVersion { get; }

        public IEnumerable<GameVersion> KnownVersions { get; }
        public string ExecutableLocation { get; }

        private readonly IConfiguration _configuration;

        public GameInstallation(IConfiguration configuration)
        {
            // TODO restore some mirror capability
            _configuration = configuration;

            ExecutableLocation = _configuration["executable_location"];
            var installedChecksum = GetAuroraChecksum();
            KnownVersions = _configuration.GetSection("known_version_checksums")
                                          .Get<Dictionary<string, string>>()
                                          .Select(entry => new GameVersion(entry.Key, entry.Value));
            InstalledVersion = KnownVersions.Single(version => version.Checksum == installedChecksum);
        }

        internal string GetAuroraChecksum()
        {
            var bytes = File.ReadAllBytes(ExecutableLocation);

            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(bytes);
            var str = Convert.ToBase64String(hash);

            return str.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 6);
        }
    }
}
