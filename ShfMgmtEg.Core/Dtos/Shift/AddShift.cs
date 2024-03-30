namespace ShfMgmtEg.Core.Dtos.Shift;

public class AddShift
{
    
    public string? ShiftName { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public int? TeamId { get; set; }
    public string? Description { get; set; }
    public int? PeriodicType { get; set; }
    
}