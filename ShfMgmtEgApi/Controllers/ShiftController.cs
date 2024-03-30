using Microsoft.AspNetCore.Mvc;
using ShfMgmtEgApi.Core.Entities.Models;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : Controller
{

    private static List<Shift> _shiftEntity = new List<Shift>
    {
        new Shift{Id=new Guid().ToString(),Name = "Shift 1"},
        new Shift{Id=new Guid().ToString(),Name = "Shift 2"},
        new Shift{Id=new Guid().ToString(),Name = "Shift 3"}
    };
    
   
    // GET,
    [HttpGet]
    [Route("GetAll")]
    public ActionResult<List<Shift>> Get()
    {
        return Ok(_shiftEntity);
    }
    
    // GET
    [HttpGet("{id}")]
    public ActionResult<Shift> GetSingle(string id)
    {
        var shift = _shiftEntity.FirstOrDefault(x => x.Id == id);
        return Ok(shift);
    }
    
    
  
    
}