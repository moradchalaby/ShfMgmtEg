using Microsoft.AspNetCore.Mvc;
using ShfMgmtEgApi.Core.Entities;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : Controller
{

    
    
    private static TeamEntity _teamEntity = new TeamEntity();
    // GET,
    [HttpGet]
    public ActionResult<TeamEntity> ShiftGet()
    {
        return Ok(_teamEntity);
    }
    
  
    
}