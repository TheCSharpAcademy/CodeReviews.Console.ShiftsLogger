using API.Models;

namespace API.Services;

public interface IShiftService
{
    Task<IEnumerable<Shift>> GetShifts();

    Task AddShift(Shift shift);

    Task DeleteShift(int id);

    Task UpdateShift(Shift shift);
}