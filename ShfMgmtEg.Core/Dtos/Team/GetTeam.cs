using ShfMgmtEg.Core.Dtos.Employee;

namespace ShfMgmtEg.Core.Dtos.Team;

public class GetTeam
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Entities.Models.Employee Manager { get; set; }
    public int ManagerId { get; set; }
    public List<GetEmployee> Employees { get; set; }
    
    
}