using ShfMgmtEg.Core.Dtos.Shift;
using ShfMgmtEg.Core.Response;

namespace ShfMgmtEg.Service.ShiftService;

public interface IShiftService
{
    Task<ServiceResponse<List<GetShift>>> GetAllShift();
    Task<ServiceResponse<GetShift>> GetShiftById(int id);
    Task<ServiceResponse<List<GetShift>>> AddShift(AddShift shift);

    Task<ServiceResponse<GetShift>> UpdateShift(UpdateShift updatedShift);
    
    Task<ServiceResponse<DeleteShift>> DeleteShift(int id, string deletedBy);
    
    Task<ServiceResponse<string>> AssignShiftToTeam(int shiftId, int teamId);
    
    Task<ServiceResponse<string>> UnAssignShiftFromTeam(int shiftId, int employeeId);
}