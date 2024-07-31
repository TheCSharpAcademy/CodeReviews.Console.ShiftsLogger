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
}
