using Microsoft.AspNetCore.Mvc;
using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Services.TeamService;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : Controller
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    // GET,
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Team>>>> Get()
    {
        return Ok(await _teamService.GetAllTeam());
    }

    // GET
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Team>>> GetSingle(int id)
    {
        return Ok(await _teamService.GetTeamById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Team>>> Add(AddTeam newTeam)
    {
        return Ok(await _teamService.AddTeam(newTeam));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Team>>> Update(UpdateTeam updatedTeam)
    {
        return Ok(await _teamService.UpdateTeam(updatedTeam));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<Team>>> Delete(int id, string deletedBy)
    {
        return Ok(await _teamService.DeleteTeam(id, deletedBy));
    }

    [HttpPost("assign")]
    public async Task<ActionResult<ServiceResponse<string>>> AssignTeamToShift(int teamId, int shiftId)
    {
        return Ok(await _teamService.AssignTeamToShift(teamId, shiftId));
    }

    [HttpPost("unassign")]
    public async Task<ActionResult<ServiceResponse<string>>> UnAssignTeamFromShift(int teamId, int shiftId)
    {
        return Ok(await _teamService.UnAssignTeamFromShift(teamId, shiftId));
    }
}