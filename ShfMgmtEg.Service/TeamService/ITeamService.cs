using ShfMgmtEg.Core.Dtos.Shift;
using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Response;

namespace ShfMgmtEg.Services.TeamService;

public interface ITeamService
{
    Task<ServiceResponse<List<GetTeam>>> GetAllTeam();
    Task<ServiceResponse<GetTeam>> GetTeamById(int id);
    Task<ServiceResponse<List<GetTeam>>> AddTeam(AddTeam team);

    Task<ServiceResponse<GetTeam>> UpdateTeam(UpdateTeam updatedTeam);

    Task<ServiceResponse<DeleteTeam>> DeleteTeam(int id, string deletedBy);

    Task<ServiceResponse<string>> AssignTeamToShift(int teamId, int shiftId);

    Task<ServiceResponse<string>> UnAssignTeamFromShift(int teamId, int shiftId);
}