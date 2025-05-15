using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface IShiftService
	{
	public  Task<ApiResponseDto<List<Shift>>> GetAllShifts(ShiftFilterOptions shiftOptions);
	public  Task <ApiResponseDto<Shift?>> GetShiftById( int id );
	public  Task <ApiResponseDto<Shift>> CreateShift(ShiftApiRequestDto shift );
	public Task <ApiResponseDto<Shift?>> UpdateShift(int id , ShiftApiRequestDto updatedShift);
	public Task <ApiResponseDto<string?>> DeleteShift(int id);
	}
