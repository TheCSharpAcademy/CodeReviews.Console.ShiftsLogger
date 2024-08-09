using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Interfaces;
using Shared;
using Shared.Enums;

namespace Server.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
  private readonly ShiftLoggerContext _context;
  public EmployeeRepository(ShiftLoggerContext context) : base(context)
  {
    _context = context;
  }
  public async Task<List<Employee>> GetEmployeesByShiftClassification(ShiftClassification classification)
  {
    var shiftEmployees = await _context.Employees.Where(es => es.ShiftAssignment == classification).ToListAsync();
    return shiftEmployees;
  }
}