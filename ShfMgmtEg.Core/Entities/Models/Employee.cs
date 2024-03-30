

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShfMgmtEg.Core.Enums;

namespace ShfMgmtEg.Core.Entities.Models;

public class Employee : BaseEntity
{

    public Title Title { get; set; }
    public int? TeamId { get; set; }
    
    public bool IsManager { get; set; } = false;
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public User User { get; set; }
    
    [NotMapped]
    public TeamEmployee TeamEmployee { get; set; }
    public Team? Team { get; set; }
    
    
   
    


}