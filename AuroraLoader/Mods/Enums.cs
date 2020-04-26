using System.Text.Json.Serialization;

namespace AuroraLoader.Mods
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ModType { EXE, DATABASE, UTILITY, ROOTUTILITY }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ModStatus { POWERUSER, PUBLIC, APPROVED }
}
