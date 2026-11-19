using System.Text.Json.Serialization;

namespace school.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StreamEnum
    {
        LION,
        BUFFALO,
        ELEPHANT,
        RHINO,
        GIRAFFE
    }
}
