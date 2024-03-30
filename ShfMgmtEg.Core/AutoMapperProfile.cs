using AutoMapper;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Entities.Models;

namespace ShfMgmtEg.Core;

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