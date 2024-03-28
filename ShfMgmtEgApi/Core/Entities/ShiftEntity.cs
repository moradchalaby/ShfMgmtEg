using ShfMgmtEgApi.Core.Enums;

namespace ShfMgmtEgApi.Core.Entities;

public class ShiftEntity : BaseEntity
{
    
    public string Name { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public int TeamId { get; set; }
    
    public TeamEntity Team { get; set; }
    
    public List<EmployeeEntity> Employees { get; set; }
    
    
    public string Description { get; set; }
    
    public Periot PeriodicType { get; set; } = Periot.AllDay;

}