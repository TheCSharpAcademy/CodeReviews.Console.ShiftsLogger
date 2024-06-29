using Ardalis.Result;
using SharedLibrary.Validations;

namespace ShiftLoggerUI.Services;

internal class EmployeeService
{
    private APIClient _client;

    public EmployeeService(APIClient client)
    {
        _client = client;
    }

    public async Task<Result<ICollection<EmployeeDto>>> GetAllEmployers()
    {
        try
        {
            return Result.Success<ICollection<EmployeeDto>>(await _client.GetAllEmployeesAsync());
        }
        catch (ApiException ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<EmployeeDto>> GetEmployer(int id)
    {
        try
        {
            return Result.Success(await _client.GetEmployeeAsync(id));
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
}
