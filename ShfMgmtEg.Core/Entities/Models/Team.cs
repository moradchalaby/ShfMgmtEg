using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShfMgmtEg.Core.Entities.Models;

public class Team :BaseEntity
{

    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public int ManagerId { get; set; }
    
    [NotMapped]
    public Employee Manager { get; set; }
    
    [NotMapped]
    public ICollection<TeamEmployee> TeamEmployees { get; set; }
    
    public int ShiftId { get; set; }
    
    [NotMapped]
    public Shift Shift { get; set; }
    
}