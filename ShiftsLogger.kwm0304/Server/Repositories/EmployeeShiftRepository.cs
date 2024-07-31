using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
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
}