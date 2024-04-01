namespace ShfMgmtEg.Core.Dtos.User;

public class GetUser
{
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
}