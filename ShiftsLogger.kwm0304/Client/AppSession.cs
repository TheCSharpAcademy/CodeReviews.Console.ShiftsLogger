namespace Client;

public class AppSession
{
  private readonly HttpClient _http;
  public AppSession(HttpClient http)
  {
    _http = http;
  }
    public async Task OnStart()
    {

    }
}
