using System.Configuration;

namespace ShiftsLogger.UI.Routes;

internal class ApiRoutes
{
    private readonly static string _baseURL;

    static ApiRoutes()
    {
        _baseURL = ConfigurationManager.AppSettings.Get("BaseUrl")!;
    }

    internal static string GetShiftsUrl() => _baseURL;

    internal static string GetShiftsByIdUrl(int id) => $"{_baseURL}/{id}";

    internal static string CreateShift() => _baseURL;

    internal static string DeleteShift(int id) => $"{_baseURL}/{id}";
}