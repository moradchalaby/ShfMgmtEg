using ShfMgmtEgApi.Core.Entities;
using ShfMgmtEgApi.Core.Response;

namespace ShfMgmtEgApi.Services.EmployeeService;

public class EmployeeService : IEmployeeService
{
    private static List<EmployeeEntity> _employeeEntities = new List<EmployeeEntity>();
    public async Task<ServiceResponse<List<EmployeeEntity>>> GetAllEmployee()
    {
        var serviceResponse = new ServiceResponse<List<EmployeeEntity>>
        { 
            Data = _employeeEntities,
            IsSuccess = _employeeEntities.Count > 0,
            Message = _employeeEntities.Count > 0 ? "Employee found" : "Employee not found"
        };
        serviceResponse.Data = _employeeEntities;
        return serviceResponse;
    }

    public async Task<ServiceResponse<EmployeeEntity>> GetEmployeeById(string id)
    {
        var employee = _employeeEntities.FirstOrDefault(x => x.Id == id);
        var serviceResponse = new ServiceResponse<EmployeeEntity>
        {
            IsSuccess = employee != null,
            Message = employee == null ? "Employee not found" : string.Empty,
            Data = employee
        };
        serviceResponse.Data = employee;
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<EmployeeEntity>>> AddEmployee(EmployeeEntity employee)
    {
        var serviceResponse = new ServiceResponse<List<EmployeeEntity>>
        {
            Data = _employeeEntities,
            IsSuccess = _employeeEntities.Count > 0,
            Message = _employeeEntities.Count > 0 ? "Employee added" : "Employee not added"
        };
        _employeeEntities.Add(employee);
        serviceResponse.Data = _employeeEntities;

        return serviceResponse;
    }

    public Task<ServiceResponse<EmployeeEntity>> UpdateEmployee(EmployeeEntity employee)
    {
        throw new NotImplementedException();
    }

    public void DeleteEmployee(int id)
    {
        throw new NotImplementedException();
    }
}