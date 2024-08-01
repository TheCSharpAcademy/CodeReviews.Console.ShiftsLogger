using RestSharp;

namespace ShiftsLogger.Services;

public class ApiService {

    private readonly RestClient _client;

    public ApiService(string baseUrl)
    {
        var options = new RestClientOptions(baseUrl);
        _client = new(options); 
    }
}