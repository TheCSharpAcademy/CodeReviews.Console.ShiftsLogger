using System.Net;

namespace ShiftsLoggerV2.RyanW84.Dtos;

public class ApiResponseDto<T> // Generic <T> means the output can take the shape of different data types
{
    public bool RequestFailed { get; set; }
    public HttpStatusCode ResponseCode { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; } // Nullable type to allow for no data to be returned
    public int TotalCount { get; set; }
}
