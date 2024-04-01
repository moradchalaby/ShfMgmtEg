namespace ShfMgmtEg.Core.Dtos.Shift;

public class GetShift
{
    public int? ShiftId { get; set; }
    public string? ShiftName { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public int? TeamId { get; set; }
    public string? Description { get; set; }
    public int? PeriodicType { get; set; }
}