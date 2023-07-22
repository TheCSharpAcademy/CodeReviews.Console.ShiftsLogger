using Newtonsoft.Json;
using RestSharp;
using ShiftLoggerConsole;
using ShiftLoggerConsole.Models;
using System.Globalization;

partial class Program
{
   static async Task DeleteShift()
    {
        ShowShifts(true);
        Console.WriteLine("Inform a id to delete");
        string id = Console.ReadLine();


        var jsonClient = new RestClient("https://localhost:7221/api/");

        var request = new RestRequest($"ShiftItems/{id}", Method.Delete);
        request.AddHeader("Content-Type", "application/json");

        var response = jsonClient.Delete(request);

        if (response.IsSuccessStatusCode)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Sucess!");
            await Console.Out.WriteLineAsync("Press enter to continue");
            Console.ReadLine();
        }
        else
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Ops, something went wrong! Try again or contant the programmer");
            await Console.Out.WriteLineAsync("Press enter to continue!");
            Console.ReadLine();
        }
    }

    static async Task ShowShifts(bool justShow)
    {
        var jsonClient = new RestClient("https://localhost:7221/api/");
        var request = new RestRequest("ShiftItems");
        var response = jsonClient.ExecuteAsync(request);
        HttpClient _client = new HttpClient();

        List<ShiftModel> shiftsList;

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            List<ShiftModel> shifts = JsonConvert.DeserializeObject<List<ShiftModel>>(rawResponse);

            CreateTableEngine.ShowTable(shifts, "Shifts");
            shiftsList = shifts.ToList();
        }

        if (!justShow)
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static async Task ShowEmployees()
    {

        var jsonClient = new RestClient("https://localhost:7221/api/");
        var request = new RestRequest("Employees");
        var response = jsonClient.ExecuteAsync(request);
        HttpClient _client = new HttpClient();

        List<EmployeeModel> EmployeeList;

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            List<EmployeeModelDto> employeesDto = new();
            string rawResponse = response.Result.Content;
            List<EmployeeModel> employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(rawResponse);

            foreach (var employee in employees)
            {
                EmployeeModelDto employeeDto = new()
                {
                    Id = employee.EmployeeId,
                    Name = employee.EmployeeName,
                };

                employeesDto.Add(employeeDto);
            }

            CreateTableEngine.ShowTable(employeesDto, "Employees");
        }
    }

    static async Task AddShift()
    {
        Console.Clear();
        Console.WriteLine("Start date!");
        string startDate = GetDateInput();

        Console.Clear();
        Console.WriteLine("Insert start time (hh:mm)");
        string startTime = Console.ReadLine();

        DateTime startingTimeDate = DateTime.ParseExact($"{startDate} {startTime}", "dd-MM-yyyy HH:mm",
                                           System.Globalization.CultureInfo.InvariantCulture);

        Console.Clear();
        Console.WriteLine("End date!");
        string endDate = GetDateInput();

        Console.Clear();
        Console.WriteLine("Insert end time (hh:mm)");
        string endTime = Console.ReadLine();

        DateTime endingTimeDate = DateTime.ParseExact($"{endDate} {endTime}", "dd-MM-yyyy HH:mm",
                                           System.Globalization.CultureInfo.InvariantCulture);

        TimeSpan duration = endingTimeDate - startingTimeDate;

        Console.Clear();
        ShowEmployees();
        Console.WriteLine("Insert the Id of the employee");
        int employeeId = int.Parse(Console.ReadLine());


        ShiftModelDto shift = new()
        {
            day = startingTimeDate.Day,
            startTime = startingTimeDate,
            endTime = endingTimeDate,
            duration = duration.ToString(),
            EmployeeId = employeeId
        };

        CreateNewShift(shift);

        Console.ReadLine();
        Console.Clear();
    }

    static async Task CreateEmployee()
    {
        Console.Clear();
        Console.WriteLine("What's the name of the employee?");
        string name = Console.ReadLine();

        EmployeeModel model = new()
        {
            EmployeeName = name
        };

        CreateNewEmployee(model);

        Console.ReadLine();
        Console.Clear();
    }

    static async void CreateNewShift( ShiftModelDto model )
    {

        var json = JsonConvert.SerializeObject(model);

        var jsonClient = new RestClient("https://localhost:7221/api/");
        var request = new RestRequest("ShiftItems", Method.Post).AddJsonBody(json);
        var response = await jsonClient.PostAsync(request);

        if (response.IsSuccessStatusCode)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Sucess!");
            await Console.Out.WriteLineAsync("Press enter to continue");
        }
        else
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Ops, something went wrong! Try again or contant the programmer");
            await Console.Out.WriteLineAsync("Press enter to continue!");
            Console.ReadLine();
        }
    }

    static async void CreateNewEmployee( EmployeeModel model )
    {
        var json = JsonConvert.SerializeObject(model);

        var jsonClient = new RestClient("https://localhost:7221/api/");
        var request = new RestRequest("Employees", Method.Post).AddJsonBody(json);
        var response = await jsonClient.PostAsync(request);

        if (response.IsSuccessStatusCode)
        {
            await Console.Out.WriteLineAsync("Sucess!");
            await Console.Out.WriteLineAsync("Press enter to continue");
        }
        else
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Ops, something went wrong! Try again or contant the programmer");
            await Console.Out.WriteLineAsync("Press enter to continue!");
            Console.ReadLine();
        }
    }

    static string GetDateInput()
    {
        Console.WriteLine("\nPlease insert the date in the format dd-mm-yyyy. Make sure the ending date is the same or higher than the starting date!");

        string dateInput = Console.ReadLine();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("\nInvalid date. Try again:\n");
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }
}
