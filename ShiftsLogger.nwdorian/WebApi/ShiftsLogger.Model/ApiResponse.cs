namespace ShiftsLogger.Model;
public class ApiResponse<T> where T : class
{
	public T? Data { get; set; }
	public bool Success { get; set; }
	public string? Message { get; set; }
}
