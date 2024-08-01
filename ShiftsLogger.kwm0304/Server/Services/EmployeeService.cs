using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Shared;

namespace Server.Services;

public class EmployeeService : Service<Employee>, IEmployeeService
{
    public EmployeeService(IRepository<Employee> repository) : base(repository)
    {
    }
}
