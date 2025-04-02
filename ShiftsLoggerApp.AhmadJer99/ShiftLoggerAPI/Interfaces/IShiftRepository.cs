using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interfaces;

public interface IShiftRepository
{
    Task<ICollection<Shift>> GetShiftsAsync();
    Task<Shift> CreateShiftAsync(Shift shift);
    Task<Shift> FindShiftAsync(int id);
    Task<string> DeleteShiftAsync(int id);
    Task<Shift> UpdateShiftAsync(int id,Shift shift);
    Task<List<Shift>> FindEmpShiftsAsync(int empId);
}
