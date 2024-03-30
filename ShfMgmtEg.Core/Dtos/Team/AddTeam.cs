using ShfMgmtEg.Core.Dtos.Employee;

namespace ShfMgmtEg.Core.Dtos.Team;

public class AddTeam
{
    
    public string Name { get; set; }
    public string Description { get; set; }
    public GetEmployee Manager { get; set; }
    public int ManagerId { get; set; }
    public List<GetEmployee> Employees { get; set; }
    
    
}