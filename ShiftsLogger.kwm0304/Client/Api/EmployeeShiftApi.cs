using Shared;

namespace Client.Api;

public class EmployeeShiftApi(HttpClient http) : IBaseApi<EmployeeShift>(http, "/employee-shift")
{
    internal async Task<List<EmployeeShift>> GetEmployeeShiftByIds()
    {
        throw new NotImplementedException();
    }

    internal async Task<List<EmployeeShift>> GetLateEmployees(int id)
    {
        throw new NotImplementedException();
    }

    internal async Task<List<EmployeeShift>> GetShiftEmployees(int shiftId)
    {
        throw new NotImplementedException();
    }

}
