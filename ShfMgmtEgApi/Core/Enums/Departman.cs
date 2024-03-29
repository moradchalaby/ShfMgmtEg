using System.Text.Json.Serialization;

namespace ShfMgmtEgApi.Core.Enums;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Departman
{
    IT = 1,
    HR = 2,
    Technic = 3,
}