using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ShfMgmtEgApi.Core.Entities;

public class UserEntity : IdentityUser
{
    [Required]
    [DisplayName("Ad soyad")]
    [StringLength(60)]
    public string FullName { get; set; } = "Ad Soyad";
}