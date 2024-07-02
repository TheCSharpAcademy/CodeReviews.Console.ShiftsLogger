using Ardalis.Result;
using SharedLibrary.Validations;

namespace ShiftLoggerUI.Services;

internal class ShiftService(APIClient client)
{
    readonly APIClient _client = client;

    public async Task<Result<ICollection<ShiftDto>>> GetAllShifts()
    {
        try
        {
            return Result.Success<ICollection<ShiftDto>>(await _client.GetAllShiftsAsync());
        }
        catch (ApiException ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<ShiftDto>> GetShift(int id)
    {
        try
        {
            return Result.Success(await _client.GetShiftAsync(id));
        }
        catch (ApiException ex) when (ex.StatusCode == 404)
        {
            return Result.NotFound(ex.Message);
        }
        catch (EmployeeValidationException ex)
        {
            return Result.Error(ex.Message);
        }

    }

    public async Task<Result<CreateShiftDto>> CreateEmployer(CreateShiftDto createShiftDto)
    {
        try
        {
            await _client.CreateShiftAsync(createShiftDto);
            return Result.Success();
        }
        catch (ApiException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
