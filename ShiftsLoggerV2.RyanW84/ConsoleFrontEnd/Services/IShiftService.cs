using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

namespace ConsoleFrontEnd.Services;

public interface IShiftService
{
    // This interface defines the contract for shift-related operations, including retrieving, creating, updating, and deleting shifts.

    // It uses asynchronous methods to handle operations that may involve I/O or network communication.
    public Task<ApiResponseDto<List<Shifts?>>> GetAllShifts(ShiftFilterOptions shiftOptions);
    public Task<ApiResponseDto<List<Shifts?>>> GetShiftById(int id);
    public Task<ApiResponseDto<Shifts>> CreateShift(Shifts shift);
    public Task<ApiResponseDto<Shifts?>> UpdateShift(int id, Shifts updatedShift);
    public Task<ApiResponseDto<string?>> DeleteShift(int id);
}
