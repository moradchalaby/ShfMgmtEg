using ShfMgmtEgApi.Core.Enums;

namespace ShfMgmtEgApi.Core.Entities.Models;

public class Shift : BaseEntity
{
    
    public string Name { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public int TeamId { get; set; }
    
    public Team Team { get; set; }
    
    public string ManagerId { get; set; }
    public Employee Manager { get; set; }
    
    public List<Employee> Employees { get; set; }
    
    
    public string Description { get; set; }
    
    public Periot PeriodicType { get; set; } = Periot.AllDay;

}