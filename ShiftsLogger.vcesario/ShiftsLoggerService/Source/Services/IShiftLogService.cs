public interface IShiftLogService
{
    public List<Shift> GetShifts();
    public Shift? GetShiftById(int id);
    public Shift CreateShift(Shift shift);
    public Shift? UpdateShift(Shift updatedShift);
    public string? DeleteShift(int id);
}