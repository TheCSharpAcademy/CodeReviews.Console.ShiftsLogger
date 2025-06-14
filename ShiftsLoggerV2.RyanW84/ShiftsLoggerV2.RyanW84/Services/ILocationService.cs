using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface ILocationService
{
    public Task<ApiResponseDto<List<Locations>>> GetAllLocations(
        LocationFilterOptions locationOptions
    );
    public Task<ApiResponseDto<Locations?>> GetLocationById(int id);
    public Task<ApiResponseDto<Locations>> CreateLocation(LocationApiRequestDto locationDto);
    public Task<ApiResponseDto<Locations?>> UpdateLocation(
        int id,
        LocationApiRequestDto updatedLocation
    );
    public Task<ApiResponseDto<string?>> DeleteLocation(int id);
}
