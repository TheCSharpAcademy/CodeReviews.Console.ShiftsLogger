﻿using ConsoleTableExt;
using ShiftsLogger.API.DTOs.Shift;
using ShiftsLogger.API.DTOs.Worker;
using System.Net.Http.Json;
using System.Text.Json;

namespace ShiftsLogger.UI;
public static class UI
{
    public static void Start()
    {
        HttpClient client = new HttpClient();

        bool keepGoing = true;

        while (keepGoing)
        {
            Console.WriteLine("\nMain Menu");
            Console.WriteLine("---------\n");
            Console.WriteLine("Make sure to add a worker before you add a shift!\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("Type 1 to View all Shifts");
            Console.WriteLine("Type 2 to Insert Shift");
            Console.WriteLine("Type 3 to Delete a Shift");
            Console.WriteLine("Type 4 to Update a Shift");
            Console.WriteLine("Type 5 to View all Workers");
            Console.WriteLine("Type 6 to Insert a Worker");
            Console.WriteLine("Type 7 to Delete a Worker");
            Console.WriteLine("Type 8 to Update a Worker");
            Console.WriteLine("Type 0 to Close Application");
            Console.WriteLine("---------------------------");

            Console.Write("Enter a number: ");
            string? command = Console.ReadLine();
            Console.Clear();

            switch (command)
            {
                case "0":
                    keepGoing = false;
                    break;
                case "1":
                    GetAllShifts();
                    break;
                case "2":
                    InsertShift();
                    break;
                case "3":
                    DeleteShift();
                    break;
                case "4":
                    UpdateShift();
                    break;
                case "5":
                    GetAllWorkers();
                    break;
                case "6":
                    InsertWorker();
                    break;
                case "7":
                    DeleteWorker();
                    break;
                case "8":
                    UpdateWorker();
                    break;
                default:
                    Console.WriteLine("Invalid command.Try again.");
                    break;
            }
        }

        void UpdateShift()
        {
            var endpoint = new Uri("https://localhost:7184/api/ShiftsLogger");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine("API Service not avaiable.Try again later.");
            }
            else
            {
                GetAllShifts();
                string shiftId = GetShiftId();

                try
                {
                    var result = client.GetAsync($"{endpoint}/{shiftId}").Result;

                    while (!result.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Invlaid id. Try again.");
                        shiftId = GetShiftId();

                        result = client.GetAsync($"{endpoint}/{shiftId}").Result;
                    }
                    var json = result.Content.ReadAsStringAsync().Result;

                    UpdateShiftDto? updateShift = JsonSerializer.Deserialize<UpdateShiftDto>(json);

                    string? startDate = GetStartDate();
                    if (startDate == null) CloseApp();

                    string? endDate = GetEndDate();

                    while (!Validate.IsValidDateRange(DateTime.Parse(startDate), DateTime.Parse(endDate)))
                    {
                        Console.WriteLine("Not a valid end date. Try again.");
                        endDate = GetEndDate();
                    }
                    string? workerId = GetWorkerId();

                    updateShift.Id = int.Parse(shiftId);
                    updateShift.Start = DateTime.Parse(startDate);
                    updateShift.End = DateTime.Parse(endDate);
                    updateShift.WorkerId = int.Parse(workerId);

                    JsonContent content = JsonContent.Create(updateShift);

                    var updatedContact = client.PutAsync($"{endpoint}/{shiftId}", content);

                    if (updatedContact != null)
                    {
                        Console.WriteLine("Record updated!");
                    }
                    else
                    {
                        Console.WriteLine("Error: Fail to update record!");
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Something unexpected happend.Try again later.");
                }
            }
        }

        void DeleteShift()
        {
            var endpoint = new Uri("https://localhost:7184/api/ShiftsLogger");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                GetAllShifts();
                string? shiftId = GetShiftId();

                try
                {
                    var result = client.GetAsync($"{endpoint}/{shiftId}").Result;

                    while (!result.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Invlaid id. Try again.");
                        shiftId = GetShiftId();

                        result = client.GetAsync($"{endpoint}/{shiftId}").Result;
                    }
                    var deleteContact = client.DeleteAsync($"{endpoint}/{shiftId}");

                    if (deleteContact != null)
                    {
                        Console.WriteLine("Record deleted!");
                    }
                    else
                    {
                        Console.WriteLine("Error: Fail to delete record!");
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("Something unexpected happend.Try again later.");
                }

            }
        }

        void InsertShift()
        {
            var endpoint = new Uri("https://localhost:7184/api/ShiftsLogger");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                GetAllWorkers();
                string? startDate = GetStartDate();
                if (startDate == null) CloseApp();

                string? endDate = GetEndDate();

                while (!Validate.IsValidDateRange(DateTime.Parse(startDate), DateTime.Parse(endDate)))
                {
                    Console.WriteLine("End date can't be earlier than start date. Try again.");
                    endDate = GetEndDate();
                }
                string? workerId = GetWorkerId();

                var newShift = new AddShiftDto
                {
                    Start = DateTime.Parse(startDate),
                    End = DateTime.Parse(endDate),
                    WorkerId = int.Parse(workerId)
                };

                JsonContent content = JsonContent.Create(newShift);

                try
                {
                    var insertRecord = client.PostAsync($"{endpoint}", content);

                    if (insertRecord == null)
                    {
                        Console.WriteLine("Fail to insert record to db");
                    }
                    else
                    {
                        Console.WriteLine("Record inserted to db");
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Something unexpected happend.Try again later.");
                }

            }
        }

        void GetAllShifts()
        {
            var tableData = new List<List<object>>();
            var endpoint = new Uri("https://localhost:7184/api/ShiftsLogger");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                HttpResponseMessage result = new HttpResponseMessage();
                try
                {
                    result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var json = result.Content.ReadAsStringAsync().Result;

                        List<GetShiftDto>? shifts = JsonSerializer.Deserialize<List<GetShiftDto>>(json);

                        if (shifts != null && shifts.Count > 0)
                        {
                            foreach (GetShiftDto shift in shifts)
                            {
                                tableData.Add(new List<object> { shift.Id, shift.Start, shift.End, shift.Duration, shift.WorkerId });
                            }
                        }

                        ConsoleTableBuilder
                        .From(tableData)
                        .WithColumn("Shift Id", "Start Date", "End Date", "Duration", "Worker Id")
                        .ExportAndWriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something unexpected happend.Try again later.");

                }
            }


        }

        string? GetStartDate()
        {
            Console.Write("Enter a start date(format: dd/mm/yyyy HH:MM i.e 20/10/2023 14:54): ");
            string? startDate = Console.ReadLine()?.Trim();
            if (startDate == "0") return null;

            while (!Validate.IsValidateDate(startDate))
            {
                Console.WriteLine("Invalid date.Try again.");
                Console.Write("Enter a start date(format: dd/mm/yyyy HH:MM i.e 20/10/2023 14:54): ");
                startDate = Console.ReadLine();
            }
            return startDate;
        }

        string? GetEndDate()
        {
            Console.Write("Enter a end date(format: dd/mm/yyyy HH:MM i.e 20/10/2023 14:54): ");
            string? endDate = Console.ReadLine()?.Trim();
            if (endDate == "0") return null;

            while (!Validate.IsValidateDate(endDate))
            {
                Console.Write("Enter a end date(format: dd/mm/yyyy HH:MM i.e 20/10/2023 14:54): ");
                Console.Write("Enter your end date: ");
                endDate = Console.ReadLine();
            }
            return endDate;
        }

        string GetWorkerId()
        {
            Console.Write("Enter a Worker Id(See list at the top.): ");
            string? input = Console.ReadLine();

            while (!Validate.IsValidString(input) || !Validate.IsValidWorkerId(input))
            {
                Console.WriteLine("Invalid id.Try again.");
                Console.Write("Enter an id: ");
                input = Console.ReadLine();
            }
            return input;
        }

        void GetAllWorkers()
        {
            var tableData = new List<List<object>>();
            var endpoint = new Uri("https://localhost:7184/api/Workers");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                try
                {
                    var result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var json = result.Content.ReadAsStringAsync().Result;

                        List<GetWorkerDto>? workers = JsonSerializer.Deserialize<List<GetWorkerDto>>(json);

                        if (workers != null && workers.Count > 0)
                        {
                            foreach (GetWorkerDto worker in workers)
                            {
                                tableData.Add(new List<object> { worker.Id, worker.FirstName, worker.LastName });
                            }
                        }

                        ConsoleTableBuilder
                        .From(tableData)
                        .WithColumn("Worker Id", "First Name", "Last Name")
                        .ExportAndWriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something unexpected happend.Try again later.");
                }
            }
        }

        void InsertWorker()
        {
            var endpoint = new Uri("https://localhost:7184/api/Workers");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                string? firstName = GetFirstName();

                while (!Validate.IsValidString(firstName))
                {
                    Console.WriteLine("Firstname can't be empty.Try again.");
                    firstName = GetFirstName();
                }

                string? lastName = GetLastName();

                while (!Validate.IsValidString(lastName))
                {
                    Console.WriteLine("Lastname can't be empty.Try again.");
                    lastName = GetLastName();
                }

                var newWorker = new AddWorkerDto
                {
                    FirstName = firstName,
                    LastName = lastName,
                };

                JsonContent content = JsonContent.Create(newWorker);

                try
                {
                    var insertRecord = client.PostAsync($"{endpoint}", content);

                    if (insertRecord == null)
                    {
                        Console.WriteLine("Fail to insert record to db");
                    }
                    else
                    {
                        Console.WriteLine("Record inserted to db");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Something unexpected happend.Try again later.");
                }
            }
        }

        void UpdateWorker()
        {
            var endpoint = new Uri("https://localhost:7184/api/Workers");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                GetAllWorkers();
                string workerId = GetWorkerId();
                try
                {
                    var result = client.GetAsync($"{endpoint}/{workerId}").Result;

                    while (!result.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Invlaid worker id. Try again.");
                        workerId = GetShiftId();

                        result = client.GetAsync($"{endpoint}/{workerId}").Result;
                    }

                    var json = result.Content.ReadAsStringAsync().Result;

                    UpdateWorkerDto? updateWorker = JsonSerializer.Deserialize<UpdateWorkerDto>(json);

                    string? firstName = GetFirstName();

                    while (!Validate.IsValidString(firstName))
                    {
                        Console.WriteLine("Firstname can't be empty.Try again.");
                        firstName = GetFirstName();
                    }
                    string? lastName = GetLastName();

                    while (!Validate.IsValidString(lastName))
                    {
                        Console.WriteLine("Lastname can't be empty.Try again.");
                        lastName = GetLastName();
                    }

                    updateWorker.FirstName = firstName;
                    updateWorker.LastName = lastName;

                    JsonContent content = JsonContent.Create(updateWorker);

                    var updatedContact = client.PutAsync($"{endpoint}/{workerId}", content);

                    if (updatedContact != null)
                    {
                        Console.WriteLine("Record updated!");
                    }
                    else
                    {
                        Console.WriteLine("Error: Fail to update record!");
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Something unexpected happend.Try again later.");
                }
            }
        }

        void DeleteWorker()
        {
            var endpoint = new Uri("https://localhost:7184/api/Workers");

            if (!Helpers.CheckApiHealth(endpoint))
            {
                Console.WriteLine(Helpers.API_ERROR_MESSAGE);
            }
            else
            {
                GetAllWorkers();
                string workerId = GetWorkerId();

                try
                {
                    var result = client.GetAsync($"{endpoint}/{workerId}").Result;

                    while (!result.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Invlaid worker id. Try again.");
                        workerId = GetShiftId();

                        result = client.GetAsync($"{endpoint}/{workerId}").Result;
                    }

                    var deletedContact = client.DeleteAsync($"{endpoint}/{workerId}");

                    if (deletedContact != null)
                    {
                        Console.WriteLine("Record deleted!");
                    }
                    else
                    {
                        Console.WriteLine("Error: Fail to remove record!");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Something unexpected happend.Try again later.");
                }
            }
        }

        string GetShiftId()
        {
            Console.Write("Enter a shift id: ");
            string? input = Console.ReadLine();

            while (!Validate.IsValidString(input) || !Validate.IsValidShiftId(input))
            {
                Console.WriteLine("Invalid id.Try again.");
                Console.Write("Enter an id: ");
                input = Console.ReadLine();
            }
            return input;
        }

        void CloseApp()
        {
            keepGoing = false;
        }

        string? GetFirstName()
        {
            Console.Write("Enter firstname: ");
            string? firstName = Console.ReadLine();

            return firstName;
        }

        string? GetLastName()
        {
            Console.Write("Enter lastname: ");
            string? lastName = Console.ReadLine();

            return lastName;
        }


    }


}