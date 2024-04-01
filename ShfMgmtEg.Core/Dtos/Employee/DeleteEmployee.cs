namespace ShfMgmtEg.Core.Dtos.Employee;

public class DeleteEmployee
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = true;
    public DateTime DeletedAt { get; set; } = DateTime.Now;
    public string DeletedBy { get; set; }
}