using ShiftsLoggerModels;

namespace ShiftsLoggerClientLibrary.ApiClients;

public interface IShiftApiClient
{
    public Task<IEnumerable<Shift>> GetShifts();
    public Task<IEnumerable<Shift>> GetShiftsByEmployeeId(int empId);
    public Task<Shift?> GetShiftById(int id);
    public Task<Shift> AddShift(Shift shift);
    public Task<bool> DeleteShift(int id);
    public Task<Shift> UpdateShift(Shift shift);
}