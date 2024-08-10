using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class EmployeeShiftRepository : Repository<EmployeeShift>, IEmployeeShiftRepository
{
  private readonly ShiftLoggerContext _context;
  public EmployeeShiftRepository(ShiftLoggerContext context) : base(context)
  {
    _context = context;
  }

  public async Task DeleteEmployeeShift(int employeeId, int shiftId)
  {
    var employeeShift = await GetEmployeeShiftByIds(employeeId, shiftId);
    if (employeeShift != null)
    {
      _context.EmployeeShifts.Remove(employeeShift);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<EmployeeShift?> GetEmployeeShiftByIds(int employeeId, int shiftId)
  {
    return await _context.EmployeeShifts.FirstOrDefaultAsync(
      es => es.EmployeeId == employeeId && es.ShiftId == shiftId
    );
  }

  public async Task<List<EmployeeShift>> GetLateEmployeesForShiftAsync(int shiftId)
  {
    return await _context.EmployeeShifts
        .Where(es => es.ShiftId == shiftId && es.ClockInTime > es.Shift!.StartTime)
        .ToListAsync();
  }

  public async Task<List<EmployeeShift>> GetShiftsForEmployeeAsync(int employeeId)
  {
    return await _context.Set<EmployeeShift>()
    .Where(es => es.EmployeeId == employeeId)
    .ToListAsync();
  }
  
  public async Task<List<EmployeeShift>> GetEmployeesForShiftAsync(int shiftId)
  {
    return await _context.Set<EmployeeShift>()
    .Where(es => es.ShiftId == shiftId)
    .ToListAsync();
  }
  
}