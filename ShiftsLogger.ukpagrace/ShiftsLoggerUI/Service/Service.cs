using System.Text.Json;
using ShiftsLoggerUI.Model;
using Spectre.Console;
using ShiftsLoggerUI.Utilities;
using System.Net.Http.Json;

namespace ShiftsLoggerUI.Service
{
    internal class ApiService
    {

        static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000/")
        };
        UserInput userInput = new UserInput();
        Validation validation = new Validation();
        List<ShiftLog> shiftLogs = new List<ShiftLog>();

        public async Task GetShiftLogs()
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("api/ShiftLogs");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    shiftLogs =  JsonSerializer.Deserialize<List<ShiftLog>>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {response}");
                }
            }
            catch (Exception ex) { 
            
                Console.WriteLine(ex.Message);
            }

        }

        public async Task DisplayLogs()
        {
            if (shiftLogs.Count < 1) await GetShiftLogs();
            var table = new Table();

            table.AddColumn(new TableColumn("Id").Centered());
            table.AddColumn(new TableColumn("EmployeeId").Centered());
            table.AddColumn(new TableColumn("StartTime").Centered());
            table.AddColumn(new TableColumn("EndTime").Centered());
            table.AddColumn(new TableColumn("Duration").Centered());
            table.AddColumn(new TableColumn("Comment").Centered());


            foreach (var shiftLog in shiftLogs)
            {
                table.AddRow(new Markup($"[blue]{shiftLog.Id}[/]"),
                    new Panel($"{shiftLog.EmployeeId}"),
                    new Markup($"[blue]{shiftLog.StartTime}[/]"),
                    new Panel($"{shiftLog.EndTime}"),
                    new Markup($"[green]{shiftLog.Duration}[/]"),
                    new Markup($"[blue]{shiftLog.Comment}[/]")
                );
            }
            AnsiConsole.Write(table);
        }


        public async Task PostShiftLog()
        {
            try
            {
                var employeeId = userInput.GetNumberInput("Input EmployeeId");
                var startTime = userInput.GetStartDate();
                var endTime = userInput.GetEndDate();


               if(!validation.ValidateRange(startTime, endTime))
               {
                    throw new Exception("End Time cannot be less than start time");
               }
                var duration = startTime - endTime;
                var comment = userInput.GetStringInput();

                var payload = new ShiftLog()
                {
                    EmployeeId = employeeId,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration,
                    Comment = comment,
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("api/ShiftLogs", payload);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{response.StatusCode} Successful");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {response}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }


        public async Task UpdateShiftLog()
        {
            try
            {
                await DisplayLogs();
                var id = userInput.GetNumberInput("Input Id");

                var idExists  = shiftLogs.FirstOrDefault(x => x.Id == id);
                if (idExists == null) {
                    throw new Exception($"Error: 404, Not Found, ShiftLog with this id does not exist");
                }
                var employeeId = userInput.GetNumberInput("Input EmployeeId");
                var startTime = userInput.GetStartDate();
                var endTime = userInput.GetEndDate();


                if (validation.ValidateRange(startTime, endTime))
                {
                    throw new Exception("Start Time cannot be greater than End time");
                }
                var comment = userInput.GetStringInput();
                var payload = new ShiftLog()
                {
                    Id = idExists.Id,
                    EmployeeId = employeeId,
                    StartTime = startTime,
                    EndTime = endTime,
                    Comment = comment,
                };

                HttpResponseMessage response = await client.PutAsJsonAsync($"api/ShiftLogs/{id}", payload);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{response.StatusCode} Successful");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {response}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }


        public async Task DeleteShiftLog()
        {
            try
            {
                await DisplayLogs();
                var id = userInput.GetNumberInput("Input Id");

                var idExists = shiftLogs.FirstOrDefault(x => x.Id == id);
                if (idExists == null)
                {
                    throw new Exception($"Error: 404, Not Found, ShiftLog with this id does not exist");
                }

                HttpResponseMessage response = await client.DeleteAsync($"api/ShiftLogs/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{response.StatusCode} Successful");
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {response}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }


        public async Task GetShiftLog()
        {
            try
            {

                var id = userInput.GetNumberInput("Input Id");
                HttpResponseMessage response = await client.GetAsync($"api/ShiftLogs/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    var shiftLog = JsonSerializer.Deserialize<ShiftLog>(content);

                    var table = new Table();
                    table.AddColumn(new TableColumn("Id").Centered());
                    table.AddColumn(new TableColumn("EmployeeId").Centered());
                    table.AddColumn(new TableColumn("StartTime").Centered());
                    table.AddColumn(new TableColumn("EndTime").Centered());
                    table.AddColumn(new TableColumn("Comment").Centered());

                    table.AddRow(new Markup($"[blue]{shiftLog.Id}[/]"),
                        new Panel($"{shiftLog.EmployeeId}"),
                        new Markup($"[blue]{shiftLog.StartTime}[/]"),
                        new Panel($"{shiftLog.EndTime}"),
                        new Markup($"[blue]{shiftLog.Comment}[/]")
                    );
                    
                    AnsiConsole.Write(table);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}, {response}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
