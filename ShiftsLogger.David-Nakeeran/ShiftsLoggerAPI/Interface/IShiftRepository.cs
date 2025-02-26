using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IShiftRepository : IRepository<Shift>
{
    public Task<List<Shift>> GetAllWithEmployeeAsync();

    public Task<Shift> GetByIdWithEmployeeAsync(long id);

    public Task<Shift> CreateWithEmployeeAsync(ShiftDTO entity);

    public Task<Shift> UpdateWithEmployeeAsync(long id, ShiftDTO entity);

    public Task<bool> DeleteShiftAsync(long id);

}