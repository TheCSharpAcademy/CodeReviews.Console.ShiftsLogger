using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class EmployeeRepository(ShiftLoggerContext context) : IEmployeeRepository
{
    private readonly ShiftLoggerContext _context = context;

}