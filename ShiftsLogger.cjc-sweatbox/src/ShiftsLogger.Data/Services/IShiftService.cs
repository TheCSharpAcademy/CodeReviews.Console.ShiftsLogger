using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.Data.Services;

/// <summary>
/// Interface to define the requirements of the shift service.
/// </summary>
public interface IShiftService
{
    #region Methods

    Task<bool> CreateAsync(Shift shift);

    Task<bool> DeleteAsync(Guid shiftId);

    Task<List<Shift>> ReturnAsync();

    Task<Shift?> ReturnByIdAsync(Guid shiftId);

    Task<bool> UpdateAsync(Shift shift);

    #endregion
}