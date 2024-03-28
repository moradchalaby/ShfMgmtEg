using Microsoft.AspNetCore.Mvc;
using ShfMgmtEgApi.Core.Entities;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : Controller
{

    private static List<ShiftEntity> _shiftEntity = new List<ShiftEntity>
    {
        new ShiftEntity{Id=1,Name = "Shift 1"},
        new ShiftEntity{Id=2,Name = "Shift 2"},
        new ShiftEntity{Id=3,Name = "Shift 3"}
    };
    
   
    // GET,
    [HttpGet]
    [Route("GetAll")]
    public ActionResult<List<ShiftEntity>> Get()
    {
        return Ok(_shiftEntity);
    }
    
    // GET
    [HttpGet("{id}")]
    public ActionResult<ShiftEntity> GetSingle(int id)
    {
        var shift = _shiftEntity.FirstOrDefault(x => x.Id == id);
        return Ok(shift);
    }
    
    
  
    
}