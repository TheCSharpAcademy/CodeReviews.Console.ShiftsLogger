namespace HKHemanthSharma.ShiftsLogger
{
    public interface IMyHttpClient
    {
        public HttpClient GetClient();
        public string GetBaseUrl();
    }
    public class MyHttpClient : IMyHttpClient
    {
        static string BaseUrl = "https://localhost:7023/api/";
        HttpClient client = new HttpClient();
        public HttpClient GetClient()
        {
            return client;
        }
        public string GetBaseUrl()
        {
            return BaseUrl;
        }
    }
}
