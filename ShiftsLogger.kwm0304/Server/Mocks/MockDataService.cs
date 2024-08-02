
// namespace Server.Mocks;

// public class MockDataService : IHostedService
// {
//   private readonly IServiceScopeFactory _serviceScopeFactory;

//   public MockDataService(IServiceScopeFactory serviceScopeFactory)
//   {
//     _serviceScopeFactory = serviceScopeFactory;
//   }

//   public async Task StartAsync(CancellationToken cancellationToken)
//   {
//     using var scope = _serviceScopeFactory.CreateScope();
//     var mockData = scope.ServiceProvider.GetRequiredService<MockData>();
//     await mockData.AssignMockEmployeeShifts();
//   }

//   public Task StopAsync(CancellationToken cancellationToken)
//   {
//     return Task.CompletedTask;
//   }
// }