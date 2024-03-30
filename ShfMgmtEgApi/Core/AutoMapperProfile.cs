using AutoMapper;
using ShfMgmtEgApi.Core.Dtos.Employee;
using ShfMgmtEgApi.Core.Dtos.Team;
using ShfMgmtEgApi.Core.Entities.Models;

namespace ShfMgmtEgApi.Core;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, GetEmployee>();
        CreateMap<AddEmployee, Employee>();
        CreateMap<Team, GetTeam>();
        CreateMap<AddTeam, Team>();
        
        
    }
    
}