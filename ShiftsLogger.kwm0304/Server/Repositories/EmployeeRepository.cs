using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly ShiftLoggerContext _context;
    public EmployeeRepository(ShiftLoggerContext context) : base(context)
    {
      _context = context;
    }

}