using Server.Data;
using Server.Repositories.Interfaces;
using Shared;

namespace Server.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly ShiftLoggerContext _context;
    public EmployeeRepository(ShiftLoggerContext context) : base(context)
    {
      _context = context;
    }

}