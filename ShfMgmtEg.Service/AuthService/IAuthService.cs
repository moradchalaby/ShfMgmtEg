using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Response;

namespace ShfMgmtEg.Service.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(RegisterUser user);
    Task<ServiceResponse<string>> Login(LoginUser user);
    Task<ServiceResponse<string>> ChangePassword(int id, string currentPassword, string newPassword);
    Task<ServiceResponse<string>> ResetPassword(int id, string newPassword);
}