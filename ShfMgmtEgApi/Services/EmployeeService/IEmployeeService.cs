using ShfMgmtEgApi.Core.Dtos.Employee;
using ShfMgmtEgApi.Core.Response;

namespace ShfMgmtEgApi.Services.EmployeeService;

public interface IEmployeeService
{
    Task<ServiceResponse<List<GetEmployee>>> GetAllEmployee();
    Task<ServiceResponse<GetEmployee>> GetEmployeeById(string id);
    Task<ServiceResponse<List<GetEmployee>>> AddEmployee(AddEmployee employee);

    Task<ServiceResponse<GetEmployee>> UpdateEmployee(UpdateEmployee updatedEmployee);
    
    Task<ServiceResponse<DeleteEmployee>> DeleteEmployee(string id, string deletedBy);
    
}