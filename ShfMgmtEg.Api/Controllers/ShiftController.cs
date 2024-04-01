using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.Shift;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Service.ShiftService;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : Controller
{
   
    private readonly IShiftService _shiftService;
    
    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }
    
    // GET,
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Shift>>>> Get()
    {
        return Ok(await _shiftService.GetAllShift());
    }

    // GET
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Shift>>> GetSingle(int id)
    {
        return  Ok(await _shiftService.GetShiftById(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Shift>>> Add(AddShift newShift)
    {
       
            
        return  Ok(await _shiftService.AddShift(newShift));

    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Shift>>> Update(UpdateShift updatedShift)
    {
        return Ok(await _shiftService.UpdateShift(updatedShift));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<Shift>>> Delete(int id, string deletedBy)
    {
        return Ok(await _shiftService.DeleteShift(id, deletedBy));
    }
    
    [HttpPost("assign")]
    public async Task<ActionResult<ServiceResponse<string>>> AssignShiftToTeam(int shiftId, int employeeId)
    {
        return Ok(await _shiftService.AssignShiftToTeam(shiftId, employeeId));
    }
    
    [HttpPost("unassign")]
    public async Task<ActionResult<ServiceResponse<string>>> UnAssignShiftFromTeam(int shiftId, int employeeId)
    {
        return Ok(await _shiftService.UnAssignShiftFromTeam(shiftId, employeeId));
    }
    
   
    
    
    
    
  
    
}