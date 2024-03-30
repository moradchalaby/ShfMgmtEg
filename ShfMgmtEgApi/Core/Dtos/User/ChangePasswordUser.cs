namespace ShfMgmtEgApi.Core.Dtos.User;

public class ChangePasswordUser
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    
}