using ShfMgmtEgApi.Core.Entities;
using ShfMgmtEgApi.Core.Response;

namespace ShfMgmtEgApi.Services.EmployeeService;

public interface IEmployeeService
{
    Task<ServiceResponse<List<EmployeeEntity>>> GetAllEmployee();
    Task<ServiceResponse<EmployeeEntity>> GetEmployeeById(string id);
    Task<ServiceResponse<List<EmployeeEntity>>> AddEmployee(EmployeeEntity employee);
    Task<ServiceResponse<EmployeeEntity>> UpdateEmployee(EmployeeEntity employee);
    void DeleteEmployee(int id);
    
    
}