using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ServiceResponse<int>> Register(RegisterUser user)
    {
        CreatePasswordHash(user.Password, out var passwordHash, out var passwordSalt);
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
        

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        var response = new ServiceResponse<int>
        {
            Data = newUser.Id, 
            Message = "Kullanıcı başarıyla oluşturuldu." , 
            IsSuccess = true,
            StatusCode = HttpStatusCode.Created
        };
        return response;
    }

    public Task<ServiceResponse<string>> Login(LoginUser user)
    {
        var currentUser =  _context.Users.FirstOrDefault(x =>
            string.IsNullOrEmpty(user.UserName) 
            || string.IsNullOrEmpty(user.Password)
            || x.UserName.ToLower() == user.UserName.ToLower() 
            || x.Email.ToLower() == user.UserName.ToLower() 
            || x.PhoneNumber == user.UserName);
        var userRoles = _context.RoleUser.Where(ru => ru.UserId == currentUser.Id);
        var roles = _context.Roles.Where(ro => userRoles.Any(ur => ur.RoleId == ro.Id));
        var response = new ServiceResponse<string>();
        if (currentUser == null)
        {
            response.Message = "Kullanıcı Bulunamadı.";
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
        }
        else if (!VerifyPasswordHash(user.Password, currentUser.PasswordHash, currentUser.PasswordSalt))
        {
            response.Message = "Hatalı şifre.";
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
        }
        else
        {
            response.Message = "Giriş Başarılı.";
            response.Data = CreateToken(currentUser, roles.ToList());
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
        }
        return Task.FromResult(response);
    }
    
    public Task<ServiceResponse<string>> ChangePassword(int id, string currentPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> ResetPassword(int id, string newPassword)
    {
        throw new NotImplementedException();
    }
    

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (var i = 0; i < computedHash.Length; i++)
            if (computedHash[i] != passwordHash[i])
                return false;
        return true;
    }
    
    private string CreateToken(User user, List<Role>? roles = null)
    {
        
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
        };
        roles.ForEach(f=> claims.Add(new Claim(ClaimTypes.Role, f.Name)));
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value ?? throw new InvalidOperationException()));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
            
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
}