using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ShfMgmtEg.Core.Enums;

namespace ShfMgmtEg.Core.Entities.Models;

public class User : BaseEntity
{
    public string FullName => $"{FirstName} {LastName}";
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

    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    public int? RoleId { get; set; }
    public Role? Role { get; set; }
    
    public ICollection<Permission>? Permissions { get; set; }
    
}