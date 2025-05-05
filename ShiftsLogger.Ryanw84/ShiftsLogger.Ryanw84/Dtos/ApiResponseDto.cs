using System.Net;

namespace ShiftsLogger.Ryanw84.Dtos;

public class ApiResponseDto<T>
    {
    public bool RequestFailed { get; set; } = false;
    public HttpStatusCode ResponseCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T? Data { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    }