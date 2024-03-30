using Bogus;
using Microsoft.AspNetCore.Mvc;

using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Service.EmployeeService;

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
    public async Task<ActionResult<ServiceResponse<List<Employee>>>> Get()
    {
        return Ok(await _employeeService.GetAllEmployee());
    }

    // GET
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Employee>>> GetSingle(int id)
    {
       return  Ok(await _employeeService.GetEmployeeById(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Employee>>> Add(AddEmployee newEmployee)
    {
        //if (newEmployee != null) return Ok(await _employeeService.AddEmployee(newEmployee));
        var userFaker = new Faker<User>()
            .RuleFor(x=>x.Id, f=>f.Random.Int())
            .RuleFor(x => x.FullName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.UserName, f => f.Internet.UserName());
        var newUser = userFaker.Generate();
        var employeeFaker = new Faker<AddEmployee>();

        newEmployee = employeeFaker.Generate();
        newEmployee.UserId = newUser.Id;
            
        return  Ok(await _employeeService.AddEmployee(newEmployee));

    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Employee>>> Update(UpdateEmployee updatedEmployee)
    {
        return Ok(await _employeeService.UpdateEmployee(updatedEmployee));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<Employee>>> Delete(int id, string deletedBy)
    {
        return Ok(await _employeeService.DeleteEmployee(id, deletedBy));
    }
    
    
}