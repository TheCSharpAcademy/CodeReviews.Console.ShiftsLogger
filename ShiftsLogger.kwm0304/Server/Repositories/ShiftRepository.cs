using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;
using Shared;
using Shared.Enums;

namespace Server.Repositories;

public class ShiftRepository : Repository<Shift>, IShiftRepository
{
  private readonly ShiftLoggerContext _context;
  public ShiftRepository(ShiftLoggerContext context) : base(context)
  {
    _context = context;
  }

  public async Task<List<Shift>> GetShiftsByClassification(ShiftClassification classification)
  {
    var shifts = await _context.Shifts.Where(s => s.Classification == classification).ToListAsync();
    return shifts;
  }

  public async Task<Shift?> GetLatestShiftAsync()
{
    var newestShift = await _context.Shifts.OrderByDescending(s => s.ShiftId).FirstOrDefaultAsync();
    if (newestShift != null)
    {
      return newestShift;
    }
    return null;
}
}