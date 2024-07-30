using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class EmployeeShiftService(IEmployeeShiftRepository repository) : IEmployeeService
{
    private readonly IEmployeeShiftRepository _repository = repository;
}
