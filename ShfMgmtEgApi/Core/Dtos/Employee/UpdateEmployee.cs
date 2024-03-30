using ShfMgmtEgApi.Core.Dtos.User;
using ShfMgmtEgApi.Core.Enums;

namespace ShfMgmtEgApi.Core.Dtos.Employee;

public class UpdateEmployee
{
    public string Id { get; set; }
    public string? TeamId { get; set; }
    public bool IsManager { get; set; } = false;
    public Title Title { get; set; }
    public bool IsDeleted { get; set; } = false;
    public UpdateUser? User { get; set; }
    
}