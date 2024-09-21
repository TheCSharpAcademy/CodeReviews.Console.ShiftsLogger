using System.Configuration;

namespace ShiftsLogger.UI.Utils;

internal class ApiRoutes
{
    private readonly static string _baseURL;

    static ApiRoutes()
    {
        _baseURL = ConfigurationManager.AppSettings.Get("BaseUrl")!;
    }

    internal static string GetShiftsUrl() => _baseURL;

    internal static string GetShiftsByIdUrl(int id) => $"{_baseURL}/{id}";

    internal static string CreateShiftUrl() => _baseURL;

    internal static string DeleteShiftUrl(int id) => $"{_baseURL}/{id}";
}