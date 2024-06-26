using ShiftLoggerUI;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Waiting for server...");
await Task.Delay(TimeSpan.FromSeconds(5));

var httpClient = new HttpClient();
var client = new APIClient("https://localhost:7045", httpClient);

var allE = await client.GetAllEmployeesAsync();



foreach (var item in allE)
{
    Console.WriteLine(item);
}

Console.ReadKey();  