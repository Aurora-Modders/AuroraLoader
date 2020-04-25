using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace AuroraLoader
{
    public class AuroraVersionUtilities
    {
        public static string GetChecksum(byte[] bytes)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(bytes);
            var str = Convert.ToBase64String(hash);

            return str.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 6);
        }

        public static AuroraVersion GetCurrentlyInstalledAuroraVersion(IConfiguration configuration, AuroraLoaderRegistry registry)
        {
            var checksum = GetChecksum(File.ReadAllBytes(configuration["executable_location"]));
            if (!registry.AuroraVersions.Any(v => v.Checksum.Equals(checksum)))
            {
                registry.UpdateKnownVersions();
            }
            return registry.AuroraVersions.Single(v => v.Checksum.Equals(checksum));
        }
    }
}

