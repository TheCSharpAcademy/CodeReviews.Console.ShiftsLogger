namespace ShiftsLogger.UI;
public static class Helpers
{
    static HttpClient client = new HttpClient();
    public const string API_ERROR_MESSAGE = "API Service not avaiable.Try again later.";

    public static bool CheckApiHealth(Uri uri)
    {
        var response = new HttpResponseMessage();
        try
        {
            response = client.GetAsync(uri).Result;
        }
        catch (Exception)
        {

            return false;
        }
        return response.IsSuccessStatusCode;
    }
}

