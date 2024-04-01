using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetUser>>> GetAllUser()
    {
        var response = new ServiceResponse<List<GetUser>>();
        var users = await _context.Users.ToListAsync();
        response.Data = users.Select(x => _mapper.Map<GetUser>(x)).ToList();
        response.IsSuccess = users.Count > 0;
        return response;
    }

    public async Task<ServiceResponse<GetUser>> GetUserById(int id)
    {
        var response = new ServiceResponse<GetUser>();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        response.Data = _mapper.Map<GetUser>(user);
        return response;
    }

    public async Task<bool> ExistUser(string param)
    {
        var exist = await _context.Users.AnyAsync(
            x => x.Email == param || x.UserName == param || x.PhoneNumber == param);
        return exist;
    }
}