using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Entities.Models;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : Controller
{
    private readonly DbContext _context;

    public ShiftController(DbContext context)
    {
        _context = context;
        
    }
   
    
    
    
    
  
    
}