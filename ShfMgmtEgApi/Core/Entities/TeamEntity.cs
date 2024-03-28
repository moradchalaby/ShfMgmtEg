using System.ComponentModel.DataAnnotations;

namespace ShfMgmtEgApi.Core.Entities;

public class TeamEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public int ManagerId { get; set; }
    
    public EmployeeEntity Manager { get; set; }
    
    public List<EmployeeEntity> Employees { get; set; }
    public static TeamEntity Empty { get; set; }
}