using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Enums;

namespace ShfMgmtEg.Core.Dtos.Employee;

public class UpdateEmployee
{
    public int Id { get; set; }
    public int? TeamId { get; set; }
    public bool IsManager { get; set; } = false;
    public Title Title { get; set; }
    public bool IsDeleted { get; set; } = false;
    public UpdateUser? User { get; set; }

    public bool TeamChanged { get; set; } = false;
}