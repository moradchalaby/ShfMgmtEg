using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Dtos.User;

namespace ShfMgmtEg.Core.Dtos.Team;

public class UpdateTeam
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;
    public UpdateUser? User { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<GetEmployee> Employees { get; set; }

    public int? ShiftId { get; set; }

    public bool DeleteShift { get; set; } = false;
}