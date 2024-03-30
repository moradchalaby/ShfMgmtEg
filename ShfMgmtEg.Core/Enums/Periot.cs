using System.Text.Json.Serialization;

namespace ShfMgmtEg.Core.Enums;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Periot
{
    AllDay = 1,
    AllWeek = 2,
    AllMonth = 3,
    AllYear = 4,
    False = 0,
    
}