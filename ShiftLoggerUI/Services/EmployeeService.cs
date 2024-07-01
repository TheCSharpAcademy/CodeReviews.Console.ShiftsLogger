using Ardalis.Result;
using SharedLibrary.Validations;

namespace ShiftLoggerUI.Services;

internal class EmployeeService(APIClient client)
{
    readonly APIClient _client = client;

    public async Task<Result<ICollection<EmployeeDto>>> GetAllEmployees()
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

    public async Task<Result<CreateEmployeeDto>> CreateEmployer(CreateEmployeeDto createEmployeeDto)
    {
        try
        {
            await _client.CreateEmployeeAsync(createEmployeeDto);
            return Result.Success();
        }
        catch (ApiException ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<UpdateEmployeeDto>> UpdateEmployer(int Id, UpdateEmployeeDto updateEmployeeDto)
    {
        try
        {
            await _client.UpdateEmployeeAsync(Id, updateEmployeeDto);
            return Result.Success();
        }
        catch (ApiException ex)
        {
            return Result.Error(ex.Message);
        }

    }

    public async Task<Result> DeleteEmployer(int Id)
    {
        try
        {
            await _client.DeleteEmployeeAsync(Id);
            return Result.Success();
        }
        catch (ApiException ex)
        {
            return Result.Error(ex.Message);
        }
    }


}
