using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Entities.Models.Relationships;

namespace ShfMgmtEg.Core.Entities.Models;

public class Team : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    [Required] public int ManagerId { get; set; }

    [NotMapped] public GetEmployee Manager { get; set; }

    [NotMapped] public ICollection<TeamEmployee> TeamEmployees { get; set; }

    [NotMapped] public List<GetEmployee> Employees { get; set; }

    public IEnumerable<ShiftTeam>? ShiftTeams { get; set; }
}