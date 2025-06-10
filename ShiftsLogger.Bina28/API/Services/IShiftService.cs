using ShiftsLogger.Bina28.Dtos;
using ShiftsLogger.Bina28.Models;

namespace ShiftsLogger.Bina28.Services;

public interface IShiftService
{	public Task<ApiResponseDto<List<Shift>>> GetAll(FilterOptions filterOptions);
	public Task<ApiResponseDto<Shift?>> GetById(int id);
	public Task<ApiResponseDto<Shift>> Create(Shift shift);
	public Task<ApiResponseDto<Shift>> Update(int id, Shift updatedShift);
	public Task<ApiResponseDto<string?>> Delete(int id);
}
