using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using Semver;

namespace AuroraLoader.Mods
{
    public static class ModConfigurationReader
    {
        public static IEnumerable<AuroraVersion> AuroraVersionsFromString(string versionChecksums)
        {
            var settings = FromKeyValueString(versionChecksums);
            return settings.Select(kvp => new AuroraVersion(SemVersion.Parse(kvp.Key), kvp.Value));
        }

        public static IList<Mirror> GetMirrorsFromIni(IConfiguration configuration)
        {
            var mirrors = new List<Mirror>();
            try
            {
                foreach (var rootUrl in File.ReadAllLines(Path.Combine(Program.AuroraLoaderExecutableDirectory, "mirrors.ini")))
                {
                    try
                    {
                        mirrors.Add(new Mirror(configuration, rootUrl));
                    }
                    catch (Exception exc)
                    {
                        Log.Error($"Failed to add {rootUrl} as a mirror", exc);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse mirror data from {Path.Combine(Program.AuroraLoaderExecutableDirectory, "mirrors.ini")}", e);
            }

            return mirrors;
        }

        public static Dictionary<string, string> FromKeyValueString(string str)
        {
            var result = new Dictionary<string, string>();

            var lines = str.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => l.Length > 0)
                .Where(l => !l.StartsWith(";"))
                .ToList();

            foreach (var line in lines)
            {
                if (!line.Contains("="))
                {
                    throw new Exception("Invalid config line: " + line);
                }

                var index = line.IndexOf('=');
                var name = line.Substring(0, index);
                var val = line.Substring(index + 1);

                result.Add(name, val);
            }

            return result;
        }

        public static string ToString(Dictionary<string, string> values)
        {
            var sb = new StringBuilder();

            foreach (var kvp in values)
            {
                sb.AppendLine(kvp.Key + "=" + kvp.Value);
            }

            return sb.ToString();
        }
    }
}
