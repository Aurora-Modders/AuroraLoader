using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuroraLoader.Mods
{
    public class Mod
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public ModStatus Status { get; set; }

        [JsonPropertyName("type")]
        public ModType Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("launch_command")]
        public string LaunchCommand { get; set; }

        [JsonPropertyName("configuration_file")]
        public string ConfigurationFile { get; set; }

        [JsonPropertyName("downloads")]
        public List<ModVersion> Downloads { get; set; } = new List<ModVersion>();

        internal Mod() { }


        // Helper props
        [JsonIgnore]
        public ModVersion LatestVersion => Downloads.OrderByDescending(v => v.Version).FirstOrDefault();
        public ModVersion LatestVersionCompatibleWith(AuroraVersion auroraVersion) => Downloads.OrderByDescending(v => v.Version)
            .Where(v => auroraVersion.CompatibleWith(v.TargetAuroraVersion))
            .FirstOrDefault();

        [JsonIgnore]
        public ModVersion LatestInstalledVersion => Downloads.OrderByDescending(v => v.Version)
            .Where(v => v.Downloaded)
            .FirstOrDefault();
        public ModVersion LatestInstalledVersionCompatibleWith(AuroraVersion auroraVersion) => Downloads.OrderByDescending(v => v.Version)
            .Where(v => v.Downloaded && auroraVersion.CompatibleWith(v.TargetAuroraVersion))
            .FirstOrDefault();

        public bool Installed => LatestInstalledVersion != null;
        public bool CanBeUpdated(AuroraVersion auroraVersion) => LatestVersion != null
                && LatestInstalledVersion != null
                && LatestInstalledVersionCompatibleWith(auroraVersion).Version.CompareByPrecedence(LatestVersionCompatibleWith(auroraVersion).Version) < 0;

        public string ModFolder => Path.Combine(Program.ModDirectory, Name);

        public void UpdateCache()
        {
            Directory.CreateDirectory(ModFolder);
            File.WriteAllText(Path.Combine(ModFolder, "mod.json"), JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                WriteIndented = true
            }));
        }

        public static Mod DeserializeMod(string rawJson)
        {
            var mod = JsonSerializer.Deserialize<Mod>(rawJson, new JsonSerializerOptions()
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true
            });
            foreach (var modVersion in mod.Downloads)
            {
                modVersion.Mod = mod;
            }
            return mod;
        }
    }
}
