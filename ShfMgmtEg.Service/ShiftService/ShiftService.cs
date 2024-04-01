using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.Shift;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Entities.Models.Relationships;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;

namespace ShfMgmtEg.Service.ShiftService;

public class ShiftService : IShiftService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private IShiftService _shiftServiceImplementation;

    public ShiftService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetShift>>> GetAllShift()
    {
        var response = new ServiceResponse<List<GetShift>>();
        var shifts = await _context.Shifts.ToListAsync();
        response.Data = shifts.Select(x => _mapper.Map<GetShift>(x)).ToList();
        return response;
    }

    public async Task<ServiceResponse<GetShift>> GetShiftById(int id)
    {
        var response = new ServiceResponse<GetShift>();
        var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.Id == id);
        response.Data = _mapper.Map<GetShift>(shift);
        return response;
    }

    public async Task<ServiceResponse<List<GetShift>>> AddShift(AddShift shift)
    {
        var response = new ServiceResponse<List<GetShift>>();
        var entity = _mapper.Map<Shift>(shift);
        await _context.Shifts.AddAsync(entity);
        await _context.SaveChangesAsync();
        response.Data = await _context.Shifts.Select(x => _mapper.Map<GetShift>(x)).ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<GetShift>> UpdateShift(UpdateShift updatedShift)
    {
        var response = new ServiceResponse<GetShift>();
        try
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.Id == updatedShift.Id);
            shift = _mapper.Map<UpdateShift, Shift>(updatedShift, shift);
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetShift>(shift);
            response.IsSuccess = true;
            response.Message = "Shift updated";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }


    public async Task<ServiceResponse<DeleteShift>> DeleteShift(int id, string deletedBy)
    {
        var response = new ServiceResponse<DeleteShift>();
        try
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(x => x.Id == id);
            if (shift != null)
            {
                shift.IsDeleted = true;
                shift.DeletedAt = DateTime.Now;
                shift.DeletedBy = deletedBy;
                _context.Shifts.Update(shift);
                await _context.SaveChangesAsync();

                _context.ShiftTeams.RemoveRange(_context.ShiftTeams.Where(x => x.ShiftId == id));
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<DeleteShift>(shift);
                response.IsSuccess = true;
                response.Message = "Shift deleted";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Shift not found";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public Task<ServiceResponse<string>> AssignShiftToTeam(int shiftId, int teamId)
    {
        _context.ShiftTeams.Add(new ShiftTeam
        {
            TeamId = teamId,
            ShiftId = shiftId
        });

        _context.SaveChangesAsync();
        return Task.FromResult(new ServiceResponse<string>
        {
            Data = "Shift assigned to team",
            IsSuccess = true,
            Message = "Shift assigned to team"
        });
    }

    public Task<ServiceResponse<string>> UnAssignShiftFromTeam(int shiftId, int employeeId)
    {
        _context.ShiftTeams.RemoveRange(_context.ShiftTeams.Where(x => x.ShiftId == shiftId && x.TeamId == employeeId));
        _context.SaveChangesAsync();
        return Task.FromResult(new ServiceResponse<string>
        {
            Data = "Shift unassigned from team",
            IsSuccess = true,
            Message = "Shift unassigned from team"
        });
    }
}