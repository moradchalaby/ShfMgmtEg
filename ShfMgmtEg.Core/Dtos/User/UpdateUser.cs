namespace ShfMgmtEg.Core.Dtos.User;

public class UpdateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public bool Status { get; set; } = true;
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}