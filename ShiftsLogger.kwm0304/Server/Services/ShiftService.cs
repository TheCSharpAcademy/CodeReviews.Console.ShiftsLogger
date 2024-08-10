using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Shared;
using Shared.Enums;

namespace Server.Services;

public class ShiftService : Service<Shift>, IShiftService
{
    private readonly IShiftRepository _shiftRepository;
    public ShiftService(IRepository<Shift> repository, IShiftRepository shiftRepository) : base(repository)
    {
        _shiftRepository = shiftRepository;
    }

    public async Task<List<Shift>> GetShiftsByClassification(ShiftClassification classification)
    {
        return await _shiftRepository.GetShiftsByClassification(classification);
    }
    public async Task<Shift?> GetNewestShift()
    {
        return await _shiftRepository.GetLatestShiftAsync();
    }
}
