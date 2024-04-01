using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Dtos.User;

namespace ShfMgmtEg.Core.Dtos.Employee;

public class GetEmployee
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TeamId { get; set; }
    public string Code { get; set; }
    public bool IsManager { get; set; }
    public Entities.Models.User User { get; set; }
    public string Title { get; set; }
    public bool IsDeleted { get; set; } = false;
}