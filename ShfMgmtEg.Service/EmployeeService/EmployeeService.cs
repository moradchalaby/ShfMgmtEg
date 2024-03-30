using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service.EmployeeService;

public class EmployeeService : IEmployeeService
{
private readonly IMapper _mapper;
private readonly DataContext _context;

public EmployeeService(IMapper mapper,DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetEmployee>>> GetAllEmployee()
    {
        
        var response = new ServiceResponse<List<GetEmployee>>();
        var employees = await _context.Employees.ToListAsync();
        response.Data = employees.Select(x => _mapper.Map<GetEmployee>(x)).ToList();
        return response;
   
    }

    public async Task<ServiceResponse<GetEmployee>> GetEmployeeById(int id)
    {
        var response = new ServiceResponse<GetEmployee>();
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
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
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == updatedEmployee.Id);
            employee = _mapper.Map<UpdateEmployee, Employee>(updatedEmployee, employee);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
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
            Employee employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                employee.DeletedAt = DateTime.Now;
                employee.DeletedBy = deletedBy;
                _context.Employees.Update(employee);
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
}