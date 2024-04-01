using FluentValidation;

namespace ShfMgmtEg.Core.Dtos.User;

public class LoginUser
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
