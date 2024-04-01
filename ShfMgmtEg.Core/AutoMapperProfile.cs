using AutoMapper;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Dtos.Shift;
using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Entities.Models;

namespace ShfMgmtEg.Core;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, GetEmployee>().ReverseMap();
        CreateMap<AddEmployee, Employee>();
        CreateMap<Team, GetTeam>().ReverseMap();
        CreateMap<AddTeam, Team>();
        CreateMap<User, GetUser>();
        CreateMap<Shift, GetShift>();
    }
}