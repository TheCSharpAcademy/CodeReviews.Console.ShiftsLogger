using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class EmployeeService : Service<Employee>, IEmployeeService
{
    public EmployeeService(IRepository<Employee> repository) : base(repository)
    {
    }
}
