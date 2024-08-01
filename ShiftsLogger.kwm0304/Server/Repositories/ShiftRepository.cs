using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;
using Shared;

namespace Server.Repositories;

public class ShiftRepository : Repository<Shift>, IShiftRepository
{
  private readonly ShiftLoggerContext _context;
  public ShiftRepository(ShiftLoggerContext context) : base(context)
  {
    _context = context;
  }
}