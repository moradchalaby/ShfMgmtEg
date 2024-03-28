using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShfMgmtEgApi.Core.Entities;
using ShfMgmtEgApi.Core.Response;
using ShfMgmtEgApi.Services.EmployeeService;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : Controller
{

    private readonly IEmployeeService _employeeService;
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    // GET,
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<EmployeeEntity>>>> Get()
    {
        return Ok(_employeeService.GetAllEmployee());
    }

    // GET
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<EmployeeEntity>>> GetSingle(string id)
    {
        
       return  Ok(_employeeService.GetEmployeeById(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<EmployeeEntity>>> Add(EmployeeEntity newEmployee)
    {
        if (newEmployee.Id == "faker")
        {
            var employeeFaker = new Faker<EmployeeEntity>()
                .RuleFor(e => e.Id, f => f.Random.Guid().ToString())
                .RuleFor(e => e.FullName, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.UserName, f => f.Internet.UserName());

            newEmployee = employeeFaker.Generate();
            
            return Ok(_employeeService.AddEmployee(newEmployee));
        }

        return BadRequest();
    }
    
}