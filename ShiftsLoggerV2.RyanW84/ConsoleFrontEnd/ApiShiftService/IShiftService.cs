using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;


namespace ConsoleFrontEnd.ApiShiftService;

public interface IShiftService
{
    Task<ApiResponseDto<List<Shifts>>> GetAllShifts(ShiftFilterOptions shiftFilterOptions);
    Task<ApiResponseDto<List<Shifts>>> GetShiftById(int id);
    Task<ApiResponseDto<Shifts>> CreateShift(Shifts createdShift);
    Task<ApiResponseDto<Shifts>> UpdateShift(int id, Shifts updatedShift);
    Task<ApiResponseDto<string>> DeleteShift(int id);
}
