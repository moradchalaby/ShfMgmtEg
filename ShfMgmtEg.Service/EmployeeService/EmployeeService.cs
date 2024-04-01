using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Entities.Models.Relationships;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service.EmployeeService;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private IEmployeeService _employeeServiceImplementation;

    public EmployeeService(IMapper mapper, DataContext context, IEmployeeService employeeServiceImplementation)
    {
        _mapper = mapper;
        _context = context;
        _employeeServiceImplementation = employeeServiceImplementation;
    }

    public async Task<ServiceResponse<List<GetEmployee>>> GetAllEmployee()
    {
        var response = new ServiceResponse<List<GetEmployee>>();
        var employees = await _context.Employees.Include(e => e.User).Include(e => e.Team).ToListAsync();
        response.Message = "All Employees";
        response.IsSuccess = true;
        response.Data = employees.Select(x =>
        {
            _mapper.Map<GetEmployee>(x);
            _mapper.Map<GetUser>(x.User);
            _mapper.Map<GetTeam>(x.Team);
            return _mapper.Map<GetEmployee>(x);
        }).ToList();
        return response;
    }

    public async Task<ServiceResponse<GetEmployee>> GetEmployeeById(int id)
    {
        var response = new ServiceResponse<GetEmployee>();
        var employee =
            await _context.Employees.Include(e => e.User).Include(e => e.Team).FirstOrDefaultAsync(x => x.Id == id) ??
            throw new InvalidOperationException();
        employee.User = _mapper.Map<User>(employee.User);
        employee.Team = _mapper.Map<Team>(employee.Team);
        response.Data = _mapper.Map<GetEmployee>(employee);
        return response;
    }

    public async Task<ServiceResponse<List<GetEmployee>>> AddEmployee(AddEmployee employee)
    {
        var response = new ServiceResponse<List<GetEmployee>>();
        var entity = _mapper.Map<Employee>(employee);
        await _context.Employees.AddAsync(entity);
        await _context.SaveChangesAsync();
        response.Data = await _context.Employees.Select(x => _mapper.Map<GetEmployee>(x)).ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<GetEmployee>> UpdateEmployee(UpdateEmployee updatedEmployee)
    {
        var response = new ServiceResponse<GetEmployee>();
        try
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == updatedEmployee.Id) ??
                           throw new InvalidOperationException();
            employee = _mapper.Map(updatedEmployee, employee);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            if (updatedEmployee.TeamChanged)
            {
                _context.TeamEmployees.RemoveRange(_context.TeamEmployees.Where(x => x.EmployeeId == employee.Id));
                await _context.SaveChangesAsync();
                _context.TeamEmployees.Add(new TeamEmployee
                {
                    EmployeeId = employee.Id,
                    TeamId = updatedEmployee.TeamId
                });
                await _context.SaveChangesAsync();
            }

            response.Data = _mapper.Map<GetEmployee>(employee);
            response.IsSuccess = true;
            response.Message = "Employee updated";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }


    public async Task<ServiceResponse<DeleteEmployee>> DeleteEmployee(int id, string deletedBy)
    {
        var response = new ServiceResponse<DeleteEmployee>();
        try
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id) ??
                           throw new InvalidOperationException();
            if (employee != null)
            {
                employee.IsDeleted = true;
                employee.DeletedAt = DateTime.Now;
                employee.DeletedBy = deletedBy;
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                _context.TeamEmployees.RemoveRange(_context.TeamEmployees.Where(x => x.EmployeeId == id));
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<DeleteEmployee>(employee);
                response.IsSuccess = true;
                response.Message = "Employee deleted";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Employee not found";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public Task<ServiceResponse<string>> AssignEmployeeToTeam(int employeeId, int teamId)
    {
        _context.TeamEmployees.RemoveRange(_context.TeamEmployees.Where(x => x.EmployeeId == employeeId));
        _context.SaveChanges();
        _context.TeamEmployees.Add(new TeamEmployee
        {
            EmployeeId = employeeId,
            TeamId = teamId
        });
        _context.SaveChanges();
        return Task.FromResult(new ServiceResponse<string>
        {
            Data = "Employee assigned to team",
            Message = "Employee assigned to team",
            IsSuccess = true
        });
    }

    public Task<ServiceResponse<string>> RemoveEmployeeFromTeam(int employeeId, int teamId)
    {
        _context.TeamEmployees.RemoveRange(
            _context.TeamEmployees.Where(x => x.EmployeeId == employeeId && x.TeamId == teamId));
        _context.SaveChanges();
        return Task.FromResult(new ServiceResponse<string>
        {
            Data = "Employee removed from team",
            Message = "Employee removed from team",
            IsSuccess = true
        });
    }
}