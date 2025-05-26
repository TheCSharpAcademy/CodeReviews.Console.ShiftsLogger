using ConsoleFrontEnd.Models;

namespace ConsoleFrontEnd.ApiShiftService;

public interface IShiftService
{
    Task<List<Shifts>> GetAllShifts();
    Task<List<Shifts>> GetShiftById(int id);
    Task<Shifts> CreateShift(Shifts createdShift);
    Task<Shifts> UpdateShift(int id, Shifts updatedShift);
    Task<string> DeleteShift(int id);
}
