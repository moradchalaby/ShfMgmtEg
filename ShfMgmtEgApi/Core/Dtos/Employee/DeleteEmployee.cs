namespace ShfMgmtEgApi.Core.Dtos.Employee;

public class DeleteEmployee
{
    public string Id { get; set; }
    public bool IsDeleted { get; set; } = true;
    public DateTime DeletedAt { get; set; } = DateTime.Now;
    public string DeletedBy { get; set; }
    
}