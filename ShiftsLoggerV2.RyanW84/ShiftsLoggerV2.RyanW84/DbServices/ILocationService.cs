using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface ILocationService
{
    Task<ApiResponseDto<List<Locations>>> GetAllLocations(LocationFilterOptions locationOptions);
    Task<ApiResponseDto<Locations?>> GetLocationById(int id);
    Task<ApiResponseDto<Locations>> CreateLocation(LocationApiRequestDto locationDto);
    Task<ApiResponseDto<Locations?>> UpdateLocation(int id, LocationApiRequestDto updatedLocation);
    Task<ApiResponseDto<string?>> DeleteLocation(int id);
}
