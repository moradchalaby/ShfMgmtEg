using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Enums;

namespace ShfMgmtEg.Core.Dtos.Shift;

public class UpdateShift
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;
    public UpdateUser? User { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int? TeamId { get; set; }
    
    
    
    
}