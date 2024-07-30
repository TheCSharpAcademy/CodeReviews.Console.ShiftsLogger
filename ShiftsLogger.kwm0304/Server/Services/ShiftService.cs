using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class ShiftService(IShiftRepository repository) : IShiftService
{
    private readonly IShiftRepository _repository = repository;
}
