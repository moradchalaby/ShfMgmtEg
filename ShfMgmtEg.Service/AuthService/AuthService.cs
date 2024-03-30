using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<ServiceResponse<int>> Register(RegisterUser user)
    {
        
        CreatePasswordHash(user.Password,out byte[] passwordHash, out byte[] passwordSalt);
        var newUser = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Address = user.Address,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        Console.WriteLine(newUser);
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        ServiceResponse<int> response = new ServiceResponse<int> { Data = newUser.Id };
        return response;
    }

    public Task<ServiceResponse<string>> Login(LoginUser user)
    {
        throw new NotImplementedException();
       
    }

    public Task<bool> UserExists(string username)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> ChangePassword(int id, string currentPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> ResetPassword(int id, string newPassword)
    {
        throw new NotImplementedException();
    }
    
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
    
}