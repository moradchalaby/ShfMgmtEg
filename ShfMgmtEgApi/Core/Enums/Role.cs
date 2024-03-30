using System.Text.Json.Serialization;

namespace ShfMgmtEgApi.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Admin = 1,
    User=2,
    Manager=3,
    Employee=4
}