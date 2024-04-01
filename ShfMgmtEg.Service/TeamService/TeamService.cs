using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Dtos.Shift;
using ShfMgmtEg.Core.Dtos.Team;
using ShfMgmtEg.Core.Entities.Models;
using ShfMgmtEg.Core.Entities.Models.Relationships;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Data;
using ShfMgmtEg.Services.TeamService;

namespace ShfMgmtEg.Service.TeamService;

public class TeamService : ITeamService
{
    
private readonly IMapper _mapper;
private readonly DataContext _context;
private ITeamService _teamServiceImplementation;

public TeamService(IMapper mapper,DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetTeam>>> GetAllTeam()
    {
        
        var response = new ServiceResponse<List<GetTeam>>();
        var teams = await _context.Teams.Include(e=>e.TeamEmployees).ToListAsync();
        response.Message = "All Teams";
        response.IsSuccess = true;
        response.Data = teams.Select(x =>
        {
            _mapper.Map<GetTeam>(x);
           GetEmployee manager = _mapper.Map<GetEmployee>(_context.Employees.FirstOrDefault(e => e.Id == x.ManagerId));
           
            x.Manager = manager;
           
            List<GetEmployee> employees = _mapper.Map<List<GetEmployee>>(_context.Employees.Where(e => x.TeamEmployees.Select(te => te.EmployeeId).Contains(e.Id)).ToList());
            x.Employees = _mapper.Map<List<GetEmployee>>(employees);
            return _mapper.Map<GetTeam>(x);
        }).ToList();
        return response;
   
    }

    public async Task<ServiceResponse<GetTeam>> GetTeamById(int id)
    {
        var response = new ServiceResponse<GetTeam>();
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
        response.Data = _mapper.Map<GetTeam>(team);
        return response;
    }

    public async Task<ServiceResponse<List<GetTeam>>> AddTeam(AddTeam team)
    {
        var response = new ServiceResponse<List<GetTeam>>();
        var entity = _mapper.Map<Team>(team);
        await _context.Teams.AddAsync(entity);
        await _context.SaveChangesAsync();
        if (team.ShiftId != null)
        {
            
                List<ShiftTeam> isSet = _context.ShiftTeams.Where(x => x.TeamId == entity.Id).Where(x=>x.ShiftId == team.ShiftId).ToList();
                if (isSet.Count == 0)
                {
                    ShiftTeam shiftTeam = new ShiftTeam();
                    shiftTeam.TeamId = entity.Id;
                    shiftTeam.ShiftId = team.ShiftId.Value;
                    await _context.ShiftTeams.AddAsync(shiftTeam);
                    await _context.SaveChangesAsync();
                }
            }
        response.Data = await _context.Teams.Select(x => _mapper.Map<GetTeam>(x)).ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<GetTeam>> UpdateTeam(UpdateTeam updatedTeam)
    {
        var response = new ServiceResponse<GetTeam>();
        try
        {
            Team team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == updatedTeam.Id) ?? throw new InvalidOperationException();
            team = _mapper.Map<UpdateTeam, Team>(updatedTeam, team);
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            if (updatedTeam.ShiftId != null)
            {
                if(updatedTeam.DeleteShift)
                {
                    List<ShiftTeam> shiftTeams = _context.ShiftTeams.Where(x => x.TeamId == updatedTeam.Id).Where(x => x.ShiftId == updatedTeam.ShiftId).ToList();
                    if (shiftTeams.Count > 0)
                    {
                        _context.ShiftTeams.RemoveRange(shiftTeams);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    List<ShiftTeam> isSet = _context.ShiftTeams.Where(x => x.TeamId == updatedTeam.Id).Where(x=>x.ShiftId == updatedTeam.ShiftId).ToList();
                    if (isSet.Count == 0)
                    {
                        ShiftTeam shiftTeam = new ShiftTeam();
                        shiftTeam.TeamId = updatedTeam.Id;
                        shiftTeam.ShiftId = updatedTeam.ShiftId.Value;
                        await _context.ShiftTeams.AddAsync(shiftTeam);
                        await _context.SaveChangesAsync();
                    }
                }
              
            }
            
            response.Data = _mapper.Map<GetTeam>(team);
            response.IsSuccess = true;
            response.Message = "Team updated";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }


    public async Task<ServiceResponse<DeleteTeam>> DeleteTeam(int id, string deletedBy)
    {
        var response = new ServiceResponse<DeleteTeam>();
        try
        {
            Team team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
            if (team != null)
            {
                team.IsDeleted = true;
                team.DeletedAt = DateTime.Now;
                team.DeletedBy = deletedBy;
                _context.Teams.Update(team);
                await _context.SaveChangesAsync();
                
                _context.TeamEmployees.RemoveRange(_context.TeamEmployees.Where(x => x.TeamId == id));
                await _context.SaveChangesAsync();
                _context.ShiftTeams.RemoveRange(_context.ShiftTeams.Where(x => x.TeamId == id));
                await _context.SaveChangesAsync();
                
                _context.Employees.Where(x => x.TeamId == id).ToList().ForEach(x =>
                {
                    x.TeamId = null;
                    _context.Employees.Update(x);
                });
                
                await _context.SaveChangesAsync();
                
                response.Data = _mapper.Map<DeleteTeam>(team);
                response.IsSuccess = true;
                response.Message = "Team deleted";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Team not found";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }

    public Task<ServiceResponse<string>> AssignTeamToShift(int teamId, int shiftId)
    {
        _context.ShiftTeams.Add(new ShiftTeam
        {
            TeamId = teamId,
            ShiftId = shiftId
        });
        
        _context.SaveChangesAsync();
        return Task.FromResult(new ServiceResponse<string>
        {
            Data = "Team assigned to shift",
            IsSuccess = true
        });
    }

    public Task<ServiceResponse<string>> UnAssignTeamFromShift(int teamId, int shiftId)
    {
        _context.ShiftTeams.RemoveRange(_context.ShiftTeams.Where(x => x.TeamId == teamId).Where(x => x.ShiftId == shiftId));
        _context.SaveChangesAsync();
        return Task.FromResult(new ServiceResponse<string>
        {
            Data = "Team unassigned from shift",
            IsSuccess = true
        });
    }
}