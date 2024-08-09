using Shared;

namespace Client.Api;

public class EmployeeApi(HttpClient http) : IBaseApi<Employee>(http, "/employees")
{
}
