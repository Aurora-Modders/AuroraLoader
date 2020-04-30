using System.Text.Json.Serialization;
using Semver;

namespace AuroraLoader.Mods
{
    public class ModVersion
    {
        [JsonPropertyName("version")]
        [JsonConverter(typeof(SemVersionJsonConverter))]
        public SemVersion Version { get; set; }

        [JsonPropertyName("target_aurora_version")]
        [JsonConverter(typeof(ModCompatibilityVersionJsonConverter))]
        public ModCompabitilityVersion TargetCompatibilityVersion { get; set; }

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }

        [JsonIgnore]
        public bool Installed { get; set; } = false;

        public bool WorksForVersion(AuroraVersion auroraVersion) => TargetCompatibilityVersion.WorksForVersion(auroraVersion.Version);
    }
}
