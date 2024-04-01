namespace ShfMgmtEg.Core.Entities.Models.Relationships;

public class TeamEmployee
{
    public int Id { get; set; }
    public int? TeamId { get; set; }
    public Team Team { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}