using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using Semver;

namespace AuroraLoader.Mods
{
    public static class ModConfigurationReader
    {
        public static ModConfiguration ModConfigurationFromIni(string modIniPath)
        {
            if (!File.Exists(modIniPath) || Path.GetFileName(modIniPath) != "mod.ini")
            {
                throw new Exception($"Path {modIniPath} not a valid mod.ini file");
            }
            var settings = FromKeyValueString(File.ReadAllText(modIniPath));

            var mod = new ModConfiguration(Path.GetDirectoryName(modIniPath))
            {
                // Required
                Name = settings["Name"] ?? "",
                Type = Enum.Parse<ModType>(settings["Type"], true),
                Status = Enum.Parse<ModStatus>(settings["Status"], true),
                Version = SemVersion.Parse(settings["Version"], false),
                TargetAuroraVersion = SemVersion.Parse(settings["AuroraVersion"], false),

                // Optional at least some of the time
                ExecuteCommand = settings.ContainsKey("Exe") ? settings["Exe"] : settings.ContainsKey("Executable") ? settings["Executable"] : null,
                Updates = settings.ContainsKey("Updates") ? settings["Updates"] : null,
                ModInternalConfigFile = settings.ContainsKey("Config") ? settings["Config"] : null
            };


            ValidateModConfiguration(mod);

            return mod;
        }

        private static void ValidateModConfiguration(ModConfiguration mod)
        {
            if (mod.Name == null || mod.Name.Length < 2)
            {
                throw new Exception("Mod name must have length at least 2");
            }
            if (mod.Version == null)
            {
                throw new Exception("Mod must have a version");
            }
            if (mod.TargetAuroraVersion == null)
            {
                throw new Exception("Mod must have an Aurora version");
            }
            if (mod.ExecuteCommand == null)
            {
                if (mod.Type == ModType.EXE || mod.Type == ModType.UTILITY || mod.Type == ModType.ROOTUTILITY)
                {
                    throw new Exception("Mod of type " + mod.Type.ToString() + " must define an Exe");
                }
            }
            if (mod.ExecuteCommand.Equals("Aurora.exe"))
            {
                throw new Exception("Mod exe can not be Aurora.exe");
            }
        }

        public static IEnumerable<AuroraVersion> AuroraVersionsFromString(string versionChecksums)
        {
            var settings = FromKeyValueString(versionChecksums);
            return settings.Select(kvp => new AuroraVersion(SemVersion.Parse(kvp.Key), kvp.Value));
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
