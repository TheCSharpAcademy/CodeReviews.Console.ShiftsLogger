using Spectre.Console;

namespace Server.Mocks;

public class MockDataService : IHostedService
{
  private readonly IServiceScopeFactory _serviceScopeFactory;

  public MockDataService(IServiceScopeFactory serviceScopeFactory)
  {
    _serviceScopeFactory = serviceScopeFactory;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    using var scope = _serviceScopeFactory.CreateScope();
    var mockData = scope.ServiceProvider.GetRequiredService<MockData>();

    bool employeesExist = await mockData.CheckIfEmployeesExist();
    bool shiftsExist = await mockData.CheckIfShiftsExist();
    bool employeeShiftsExist = await mockData.CheckIfEmployeeShiftsExist();

    if (!employeesExist)
    {
      await mockData.CreateMockEmployees();
    }
    else
    {
      AnsiConsole.WriteLine("Employees already exist, deleting and recreating...");
      await mockData.DeleteAllEmployees();
      await mockData.CreateMockEmployees();
    }
    if (!shiftsExist)
    {
      await mockData.CreateMockShifts();
    }
    else
    {
      AnsiConsole.WriteLine("Shifts already exist, deleting and recreating...");
      await mockData.DeleteAllShifts();
      await mockData.CreateMockShifts();
    }
    if (!employeeShiftsExist)
    {
      await mockData.AssignMockEmployeeShifts();
    }
    else
    {
      AnsiConsole.WriteLine("Employee shifts already exist, deleting and recreating...");
      await mockData.DeleteAllEmployeeShifts();
      await mockData.AssignMockEmployeeShifts();
    }
      AnsiConsole.WriteLine("Mock data already exists, skipping creation");
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}