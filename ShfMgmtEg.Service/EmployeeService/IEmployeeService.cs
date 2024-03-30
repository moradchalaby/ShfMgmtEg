using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Response;

namespace ShfMgmtEg.Service.EmployeeService;

public interface IEmployeeService
{
    Task<ServiceResponse<List<GetEmployee>>> GetAllEmployee();
    Task<ServiceResponse<GetEmployee>> GetEmployeeById(int id);
    Task<ServiceResponse<List<GetEmployee>>> AddEmployee(AddEmployee employee);

    Task<ServiceResponse<GetEmployee>> UpdateEmployee(UpdateEmployee updatedEmployee);
    
    Task<ServiceResponse<DeleteEmployee>> DeleteEmployee(int id, string deletedBy);
    
}
