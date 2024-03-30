using ShfMgmtEgApi.Core.Dtos.Employee;

namespace ShfMgmtEgApi.Core.Dtos.Team;

public class GetTeam
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Entities.Models.Employee Manager { get; set; }
    public string ManagerId { get; set; }
    public List<GetEmployee> Employees { get; set; }
    
    
}