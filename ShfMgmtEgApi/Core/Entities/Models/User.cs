using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ShfMgmtEgApi.Core.Enums;

namespace ShfMgmtEgApi.Core.Entities.Models;

public class User : IdentityUser
{
    [Required]
    [DisplayName("Ad")]
    [StringLength(60)]
    public string FirstName { get; set; }
    
    [Required]
    [DisplayName("Soyad")]
    [StringLength(60)]
    public string LastName { get; set; }
    [DisplayName("Adres")]
    [StringLength(200)]
    public string? Address { get; set; }
    public bool Status { get; set; } = true;
    public string FullName => $"{FirstName} {LastName}";
    public Role Role { get; set; } = Role.User;
    
    
   
    
    
}