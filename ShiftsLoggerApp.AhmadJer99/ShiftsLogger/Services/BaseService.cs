using Microsoft.Extensions.Configuration;

namespace ShiftsLoggerUI.Services;

public class BaseService
{
    protected static HttpClient? _client;
    public BaseService(IConfiguration configuration)
    {
        var baseUrl = configuration.GetSection("ApiSettings")["BaseUrl"];
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }
}