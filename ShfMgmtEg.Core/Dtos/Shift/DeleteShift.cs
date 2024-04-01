namespace ShfMgmtEg.Core.Dtos.Shift;

public class DeleteShift
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = true;
    public DateTime DeletedAt { get; set; } = DateTime.Now;
    public string DeletedBy { get; set; }
    
}