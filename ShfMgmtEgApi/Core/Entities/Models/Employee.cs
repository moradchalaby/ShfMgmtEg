

using System.ComponentModel.DataAnnotations;
using ShfMgmtEgApi.Core.Enums;

namespace ShfMgmtEgApi.Core.Entities.Models;

public class Employee : BaseEntity
{

    public Title Title { get; set; }
    public string? TeamId { get; set; }
    
    public bool IsManager { get; set; } = false;
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public User User { get; set; }
    public Team? Team { get; set; }
    
    
   
    


}