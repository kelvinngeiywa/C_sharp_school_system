using System.Text.Json.Serialization;

namespace school.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleEnum
    {
        SUPER_ADMIN,
        ADMIN,
        PARENT,
        TEACHER,
        STAFF,
        PRINCIPAL
    }
}
