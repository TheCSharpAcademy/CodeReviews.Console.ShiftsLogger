using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories;

public class ShiftRepository(ShiftLoggerContext context) : IShiftRepository
{
    private readonly ShiftLoggerContext _context = context;
}