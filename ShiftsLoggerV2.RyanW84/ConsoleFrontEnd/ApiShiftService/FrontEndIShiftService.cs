namespace ConsoleFrontEnd.ApiShiftService;

public interface FrontEndIShiftService
{
    Task GetAllShifts();
    Task GetShiftById(int id);
    Task CreateShift();
    Task UpdateShift(int id, Shift updatedShift);
    Task DeleteShift(int id);
}
