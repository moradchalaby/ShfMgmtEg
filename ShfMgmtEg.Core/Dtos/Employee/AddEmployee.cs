namespace ShfMgmtEg.Core.Dtos.Employee;

public class AddEmployee
{
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public bool? IsManager { get; set; } = false;
    public string Title { get; set; }
}