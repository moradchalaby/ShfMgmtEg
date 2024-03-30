using AutoMapper;
using ShfMgmtEgApi.Core.Dtos.Employee;
using ShfMgmtEgApi.Core.Dtos.Team;
using ShfMgmtEgApi.Core.Entities.Models;
using ShfMgmtEgApi.Core.Response;

namespace ShfMgmtEgApi.Services.EmployeeService;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private IEmployeeService _employeeServiceImplementation;

    public EmployeeService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetEmployee>>> GetAllEmployee()
    {
        var serviceResponse = new ServiceResponse<List<GetEmployee>>
        {
            IsSuccess = _employeeEntities.Count > 0,
            Message = _employeeEntities.Count > 0 ? "Employee found" : "Employee not found"
        };
        serviceResponse.Data = _employeeEntities.Select(x => _mapper.Map<GetEmployee>(x)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetEmployee>> GetEmployeeById(string id)
    {
        var employee = _employeeEntities.FirstOrDefault(x => x.Id == id);
        var serviceResponse = new ServiceResponse<GetEmployee>
        {
            IsSuccess = employee != null,
            Message = employee == null ? "Employee not found" : string.Empty
        };
        serviceResponse.Data = _mapper.Map<GetEmployee>(employee);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetEmployee>>> AddEmployee(AddEmployee employee)
    {
        var serviceResponse = new ServiceResponse<List<GetEmployee>>
        {
            IsSuccess = _employeeEntities.Count > 0,
            Message = _employeeEntities.Count > 0 ? "Employee added" : "Employee not added"
        };
        var entity = _mapper.Map<Employee>(employee);
        entity.Id = Guid.NewGuid().ToString();
        _employeeEntities.Add(entity);
        serviceResponse.Data = _employeeEntities.Select(x => _mapper.Map<GetEmployee>(x)).ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetEmployee>> UpdateEmployee(UpdateEmployee updatedEmployee)
    {
        ServiceResponse<GetEmployee> serviceResponse = new ServiceResponse<GetEmployee>();
        try
        {
            Employee employee = _employeeEntities.FirstOrDefault(x => x.Id == updatedEmployee.Id);
            if (employee != null)
            {
                employee.User = _mapper.Map<User>(updatedEmployee.User);
                employee.TeamId = updatedEmployee.TeamId;
                employee.Title = updatedEmployee.Title;
                employee.IsManager = updatedEmployee.IsManager;
                employee.IsDeleted = updatedEmployee.IsDeleted;
                serviceResponse.Data = _mapper.Map<GetEmployee>(employee);
                serviceResponse.IsSuccess = true;
                serviceResponse.Message = "Employee updated";
            }
            else
            {
                serviceResponse.IsSuccess = false;
                serviceResponse.Message = "Employee not found";
            }
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccess = false;
            serviceResponse.Message = ex.Message;
            
        }
        return serviceResponse;
    }
    

    public async Task<ServiceResponse<DeleteEmployee>> DeleteEmployee(string id, string deletedBy)
    {
        ServiceResponse<DeleteEmployee> serviceResponse = new ServiceResponse<DeleteEmployee>();
        try
        {
            Employee employee = _employeeEntities.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employeeEntities.Remove(employee);
                serviceResponse.Data = new DeleteEmployee
                {
                    Id = id,
                    IsDeleted = true,
                    DeletedAt = DateTime.Now,
                    DeletedBy = deletedBy
                };
                serviceResponse.IsSuccess = true;
                serviceResponse.Message = "Employee deleted";
            }
            else
            {
                serviceResponse.IsSuccess = false;
                serviceResponse.Message = "Employee not found";
            }
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccess = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}