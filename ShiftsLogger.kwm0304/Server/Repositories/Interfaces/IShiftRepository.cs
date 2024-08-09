using Shared;
using Shared.Enums;

namespace Server.Repositories.Interfaces;

public interface IShiftRepository : IRepository<Shift>
{
    Task<List<Shift>> GetShiftsByClassification(ShiftClassification classification);
}
