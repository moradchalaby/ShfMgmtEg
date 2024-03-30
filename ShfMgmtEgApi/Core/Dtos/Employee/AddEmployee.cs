using ShfMgmtEgApi.Core.Dtos.Team;
using ShfMgmtEgApi.Core.Dtos.User;

namespace ShfMgmtEgApi.Core.Dtos.Employee;

public class AddEmployee
{
    public string UserId { get; set; }
    public string? TeamId { get; set; }
    public bool? IsManager { get; set; }
    public GetUser User { get; set; }
    public GetTeam Team { get; set; }
    public string Title { get; set; }
}