using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Semver;

namespace AuroraLoader.Mods
{
    public class JsonSemversionConverter : JsonConverter<SemVersion>
    {
        public override SemVersion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => SemVersion.Parse(reader.GetString(), false);

        public override void Write(Utf8JsonWriter writer, SemVersion value, JsonSerializerOptions options) => value.ToString();
    }
}
