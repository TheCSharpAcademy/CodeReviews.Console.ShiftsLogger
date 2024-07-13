using Ardalis.Result;
using SharedLibrary.DTOs;
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
        catch (HttpRequestException ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<EmployeeDto>> GetEmployee(int id)
    {
        try
        {
            return Result.Success(await _client.GetEmployeeAsync(id));
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return Result.NotFound(ex.Message);
        }
        catch (EmployeeValidationException ex)
        {
            return Result.Error(ex.Message);
        }

    }

    public async Task<Result<CreateEmployeeDto>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        try
        {
            await _client.CreateEmployeeAsync(createEmployeeDto);
            return Result.Success();
        }
        catch (HttpRequestException ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<UpdateEmployeeDto>> UpdateEmployee(int Id, UpdateEmployeeDto updateEmployeeDto)
    {
        try
        {
            await _client.UpdateEmployeeAsync(Id, updateEmployeeDto);
            return Result.Success();
        }
        catch (HttpRequestException ex)
        {
            return Result.Error(ex.Message);
        }

    }

    public async Task<Result> DeleteEmploye(int Id)
    {
        try
        {
            await _client.DeleteEmployeeAsync(Id);
            return Result.Success();
        }
        catch (HttpRequestException ex)
        {
            return Result.Error(ex.Message);
        }
    }


}
