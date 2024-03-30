using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ShfMgmtEg.Core.Dtos.User;

public class RegisterUser
{
    [MinLength(3)]
    public string FirstName { get; set; }
    [MinLength(3)]
    public string LastName { get; set; }
    [LowerCase("UserName")]
    public string UserName { get; set; }
    public string? Address { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    [MinLength(6)]
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}


public class LowerCaseAttribute : Attribute
{
    public LowerCaseAttribute(string value)
    {
        value = value.ToLower();
    }
    
}


