using System.Net.Http.Headers;
using UserInterface;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("/api/Shift");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
ShiftService shiftService = new ShiftService(client);