using Microsoft.AspNetCore.Identity;

namespace ShfMgmtEg.Core.Entities.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Permission> Permissions { get; set; }
    
}