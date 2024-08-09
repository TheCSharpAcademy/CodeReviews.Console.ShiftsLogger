using Client.Api;
using Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public class Program
{
  static async Task Main(string[] args)
  {
    var serviceCollection = new ServiceCollection();
    ConfigureServices(serviceCollection);
    var serviceProvider = serviceCollection.BuildServiceProvider();
    var appSession = serviceProvider.GetRequiredService<AppSession>();
    await appSession.OnStart();
  }
  private static void ConfigureServices(IServiceCollection services)
  {
    services.AddHttpClient();
    services.AddTransient<AppSession>();
    services.AddTransient<EmployeeHandler>();
    services.AddTransient<ShiftHandler>();
    services.AddTransient<EmployeeApi>(provider =>
    {
      var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
      return new EmployeeApi(httpClient);
    });

    services.AddTransient<EmployeeShiftApi>(provider =>
    {
      var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
      return new EmployeeShiftApi(httpClient);
    });

    services.AddTransient<ShiftApi>(provider =>
    {
      var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
      return new ShiftApi(httpClient);
    });
  }
}