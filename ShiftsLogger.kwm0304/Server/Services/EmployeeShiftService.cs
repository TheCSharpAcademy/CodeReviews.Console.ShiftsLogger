using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class EmployeeShiftService : Service<EmployeeShift>, IEmployeeShiftService
{
    public EmployeeShiftService(IRepository<EmployeeShift> repository) : base(repository)
    {
    }
}
