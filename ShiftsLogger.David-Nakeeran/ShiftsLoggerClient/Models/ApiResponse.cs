using System.Net;

namespace ShiftsLoggerClient.Models;

class ApiResponse<T>
{
    public bool Success { get; set; }

    public T? Data { get; set; }

    public string Message { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}