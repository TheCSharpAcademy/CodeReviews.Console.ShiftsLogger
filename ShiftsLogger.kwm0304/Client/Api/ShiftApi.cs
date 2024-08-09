using Shared;

namespace Client.Api;

public class ShiftApi(HttpClient http) : IBaseApi<Shift>(http, "/shifts")
{

}
