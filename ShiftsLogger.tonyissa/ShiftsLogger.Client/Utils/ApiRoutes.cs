using System.Configuration;

namespace ShiftsLogger.UI.Utils;

public class ApiRoutes
{
    private readonly static string _baseURL;

    static ApiRoutes()
    {
        _baseURL = ConfigurationManager.AppSettings.Get("BaseUrl")!;
    }

    public static string GetShiftsUrl() => _baseURL;

    public static string GetShiftsByIdUrl(int id) => $"{_baseURL}/{id}";

    public static string UpdateShiftUrl(int id) => $"{_baseURL}/{id}";

    public static string CreateShiftUrl() => _baseURL;

    public static string DeleteShiftUrl(int id) => $"{_baseURL}/{id}";
}