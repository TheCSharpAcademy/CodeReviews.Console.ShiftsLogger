using Shared;

namespace Client.Api;

public class EmployeeApi(HttpClient http) : IBaseApi<Employee>(http, "/employees")
{
    internal async Task<double> GetEmployeePayForRange(string v)
    {
        throw new NotImplementedException();
    }

}
