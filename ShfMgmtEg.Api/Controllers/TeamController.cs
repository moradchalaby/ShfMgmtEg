using Microsoft.AspNetCore.Mvc;
using ShfMgmtEg.Core.Entities.Models;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : Controller
{

    
    
    private static Team _team = new Team();
    // GET,
    [HttpGet]
    public ActionResult<Team> ShiftGet()
    {
        return Ok(_team);
    }
    
  
    
}