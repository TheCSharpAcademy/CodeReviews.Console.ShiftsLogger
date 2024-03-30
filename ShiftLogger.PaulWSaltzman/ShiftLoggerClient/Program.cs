using ShiftLoggerClient;
using ShiftLoggerClient.Models;
using System.ComponentModel.Design;
using System.Net.Http.Json;


UserInterface.MainMenu();



//HttpClient client = new();
//client.BaseAddress = new Uri("https://localhost:7196");
//client.DefaultRequestHeaders.Accept.Clear();
//client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

//HttpResponseMessage response = await client.GetAsync("api/employee");
//response.EnsureSuccessStatusCode();

//if(response.IsSuccessStatusCode)
//{
//    var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeDTO>> ();
//    foreach (var employee in employees)
//    {
//        Console.WriteLine(employee.Id);
//        Console.WriteLine(employee.FirstName);
//        Console.WriteLine(employee.LastName);
//    }
//}
//else
//{
//    Console.WriteLine("no results");
//}

//Console.ReadKey();


