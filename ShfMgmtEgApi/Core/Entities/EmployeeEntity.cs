using System.Runtime.CompilerServices;

namespace ShfMgmtEgApi.Core.Entities;

public class EmployeeEntity : UserEntity
{


    public int TeamId { get; set; }
    public bool IsManager { get; set; } = false;

    public TeamEntity? Team { get; set; } = TeamEntity.Empty;



}