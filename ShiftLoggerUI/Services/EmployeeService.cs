namespace ShiftLoggerUI.Services;

internal class EmployeeService
{
    private APIClient _client;

    public EmployeeService(APIClient client)
    {
        _client = client;
    }

    public async Task<ICollection<EmployeeDto>> GetAllEmployers()
    {
        try
        {
            return await _client.GetAllEmployeesAsync();
        }
        catch (ApiException ex)
        {
            await Console.Out.WriteLineAsync($"Problem occured while trying to get GetAllEmployees. {ex.Message}");
            Console.ReadLine();
            Environment.Exit(0);
            return null;
        }
    }
}
