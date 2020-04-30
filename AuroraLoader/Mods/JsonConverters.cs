using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Semver;

namespace AuroraLoader.Mods
{
    public class ModCompatibilityVersionJsonConverter : JsonConverter<ModCompabitilityVersion>
    {
        public override ModCompabitilityVersion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new ModCompabitilityVersion(reader.GetString());

        public override void Write(Utf8JsonWriter writer, ModCompabitilityVersion value, JsonSerializerOptions options) => value.ToString();
    }

    public class SemVersionJsonConverter : JsonConverter<SemVersion>
    {
        public override SemVersion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SemVersion.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, SemVersion value, JsonSerializerOptions options) => value.ToString();
    }
}
