using System.Text.Json.Serialization;

namespace school.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountTypeEnum
    {
        PARENT,
        TEACHER,
        STAFF
    }
}
