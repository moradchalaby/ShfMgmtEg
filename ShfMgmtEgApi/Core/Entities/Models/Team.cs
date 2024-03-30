using System.ComponentModel.DataAnnotations;

namespace ShfMgmtEgApi.Core.Entities.Models;

public class Team
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public int ManagerId { get; set; }
    
    public Employee Manager { get; set; }
    
    public List<Employee> Employees { get; set; }
    
    public string ShiftId { get; set; }
    public Shift Shift { get; set; }
    
}