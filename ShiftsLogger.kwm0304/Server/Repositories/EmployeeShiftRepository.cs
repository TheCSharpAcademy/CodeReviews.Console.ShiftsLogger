using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class EmployeeShiftRepository(ShiftLoggerContext context) : IEmployeeShiftRepository
{
  private readonly ShiftLoggerContext _context = context;
}
