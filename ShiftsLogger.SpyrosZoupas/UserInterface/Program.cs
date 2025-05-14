using System.Net.Http.Headers;
using UserInterface.SpyrosZoupas;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7156/api/Shift");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
ShiftService shiftService = new ShiftService(client);
MainMenu mainMenu = new MainMenu(shiftService);
await mainMenu.ShiftsMenu();