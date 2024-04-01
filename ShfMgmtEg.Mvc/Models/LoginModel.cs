using ShfMgmtEg.Core.Response;

namespace ShfMgmtEg.Mvc.Models;

public class LoginModel : ServiceResponse<string>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? ErrorMessage { get; set; }
    
}