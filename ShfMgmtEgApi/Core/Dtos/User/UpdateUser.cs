using ShfMgmtEgApi.Core.Enums;

namespace ShfMgmtEgApi.Core.Dtos.User;

public class UpdateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public bool Status { get; set; } = true;
    public Role Role { get; set; } = Role.User;
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    
}