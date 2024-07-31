using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Services;

public class ShiftService : Service<Shift>, IShiftService
{
    public ShiftService(IRepository<Shift> repository) : base(repository)
    {
    }
}
