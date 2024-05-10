using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shiftloggerconsole.Services;
using shiftloggerconsole.UserInterface;

var basePath = AppDomain.CurrentDomain.BaseDirectory;

// Read configuration file
var config = new ConfigurationBuilder()
    .SetBasePath(basePath)
    .AddXmlFile("App.config")
    .Build();

// configuring, registering and using the HttpClientFactory
var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .BuildServiceProvider();

var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

await MainMenu.ShowMenu(httpClientFactory, config);