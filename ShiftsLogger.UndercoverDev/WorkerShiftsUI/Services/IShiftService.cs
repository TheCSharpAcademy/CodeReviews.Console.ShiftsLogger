namespace WorkerShiftsUI.Services;
public interface IShiftService
{
    public Task ViewShifts();
    public Task AddShift();
    public Task UpdateShift();
    public Task DeleteShift();
}