using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

namespace ConsoleFrontEnd.Services;

public interface ILocationService
{
    public Task<ApiResponseDto<List<Locations>>> GetAllLocations(
        LocationFilterOptions locationOptions
    );

    public Task<ApiResponseDto<List<Locations>>> GetLocationById(int id);

    public Task<ApiResponseDto<Locations>> CreateLocation(Locations createdLocation);

    public Task<ApiResponseDto<Locations?>> UpdateLocation(int id, Locations updatedLocation);

    //public Task<ApiResponseDto<string?>> DeleteLocation(int id);
}
