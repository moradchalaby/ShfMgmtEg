namespace ShfMgmtEg.Core.Entities.Models.Relationships;

public class ShiftTeam
{
    public int ShiftId { get; set; }
    public Shift Shift { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
}