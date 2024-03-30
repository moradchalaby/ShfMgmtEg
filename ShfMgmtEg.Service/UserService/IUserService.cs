using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Response;

namespace ShfMgmtEg.Service.UserService;

public interface IUserService
{
    Task<ServiceResponse<List<GetUser>>> GetAllUser();
    Task<ServiceResponse<GetUser>> GetUserById(int id);
    
    Task<bool> ExistUser(string user);
    


}