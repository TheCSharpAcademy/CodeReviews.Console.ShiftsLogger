using Shared;
using Shared.Enums;

namespace Server.Services.Interfaces;

public interface IShiftService : IService<Shift>
{
    Task<List<Shift>> GetShiftsByClassification(ShiftClassification classification);
}
