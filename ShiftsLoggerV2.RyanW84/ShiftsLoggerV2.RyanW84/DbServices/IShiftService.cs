using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface IShiftService
	{
	public  Task<ApiResponseDto<List<Shifts>>> GetAllShifts(ShiftFilterOptions shiftOptions);
	public  Task <ApiResponseDto<Shifts?>> GetShiftById( int id );
	public  Task <ApiResponseDto<Shifts>> CreateShift(ShiftApiRequestDto shift );
	public Task <ApiResponseDto<Shifts?>> UpdateShift(int id , ShiftApiRequestDto updatedShift);
	public Task <ApiResponseDto<string?>> DeleteShift(int id);
	}
