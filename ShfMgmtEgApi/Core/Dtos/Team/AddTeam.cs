using ShfMgmtEgApi.Core.Dtos.Employee;

namespace ShfMgmtEgApi.Core.Dtos.Team;

public class AddTeam
{
    
    public string Name { get; set; }
    public string Description { get; set; }
    public GetEmployee Manager { get; set; }
    public string ManagerId { get; set; }
    public List<GetEmployee> Employees { get; set; }
    
    
}