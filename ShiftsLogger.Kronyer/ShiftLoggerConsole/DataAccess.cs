using Newtonsoft.Json;
using RestSharp;
using Spectre.Console;

namespace ShiftLoggerConsole
{
    public class DataAccess
    {
        public static void RegisterNewWorker()
        {
            try
            {
                string name = AnsiConsole.Ask<string>("Type the name").Trim().ToLower();

                while (name == null)
                {
                    Console.WriteLine("Name can't be empty...");
                    name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();
                }

                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest("create", Method.Post);
                string jsonBody = "\"" + name + "\"";
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserInterface.RunMenu();
            }
        }

        public static void StartShift()
        {
            int workers = CountWorkers();
            if (workers == 0)
            {
                Console.WriteLine("There is no worker in Db, please create one first");
                Console.ReadKey();
                UserInterface.RunMenu();
            }
            AnsiConsole.MarkupLine("Start work:");
            
            string name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();

            while (name == null)
            {
                Console.WriteLine("Name can't be empty...");
                name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();
            }
            if (!WorkerExists(name))
            {
                Console.WriteLine("This worker is not registered yet, please register...");
                Console.ReadKey();
                UserInterface.RunMenu();
            }

            try
            {
                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest("start", Method.Patch);
                string jsonBody = "\"" + name + "\"";
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserInterface.RunMenu();
            }
        }

        public static void EndShift()
        {
            int workers = CountWorkers();
            if (workers == 0)
            {
                Console.WriteLine("There is no worker in Db, please create one first");
                Console.ReadKey();
                UserInterface.RunMenu();
            }
            AnsiConsole.MarkupLine("End work:");
            string name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();

            while (name == null)
            {
                Console.WriteLine("Name can't be empty...");
                name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();
            }

            if (!WorkerExists(name))
            {
                Console.WriteLine("This worker is not registered yet, please register...");
                Console.ReadKey();
                UserInterface.RunMenu();
            }

            try
            {
                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest($"end", Method.Patch);
                string jsonBody = "\"" + name + "\"";
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                client.Execute(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserInterface.RunMenu();
            }
        }

        internal static void ViewWorker()
        {
            string name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();

            while (name == null)
            {
                Console.WriteLine("Name can't be empty...");
                name = AnsiConsole.Ask<string>("Type the name:").Trim().ToLower();
            }

            try
            {
                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest($"get/{name}", Method.Get);
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseJson = response.Content;
                    var desserialize = JsonConvert.DeserializeObject<Worker>(responseJson);
                    var worker = desserialize;

                    if (worker != null)
                    {
                        var panel = new Panel($@"Name: {worker.Name}
Worked hours today({DateTime.Today:MM/dd/yyyy}): {(int)worker.WorkedHours.TotalHours:D2}:{(int)worker.WorkedHours.Minutes:D2})
Total hours worked: {(int)worker.TotalHours.TotalHours:D2}:{(int)worker.TotalHours.Minutes:D2}");
                        panel.Header("Worker info");
                        panel.Padding(1, 1, 1, 1);
                        AnsiConsole.Write(panel);
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No results found...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserInterface.RunMenu();
            }
        }

        internal static bool WorkerExists(string name)
        {
            bool exists = false;
            try
            {
                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest($"get/{name}", Method.Get);
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseJson = response.Content;
                    var desserialize = JsonConvert.DeserializeObject<Worker>(responseJson);
                    var worker = desserialize;

                    if (worker != null)
                    {
                       exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return exists;
            }
        }

        internal static void ViewAllWorkers()
        {
            try
            {
                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest($"getall", Method.Get);
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseJson = response.Content;
                    var desserialize = JsonConvert.DeserializeObject<List<Worker>>(responseJson);

                    var table = new Table();
                    table.AddColumn("Name");
                    table.AddColumn("Hours(today)");
                    table.AddColumn("Hours(total)");

                    foreach (Worker worker in desserialize)
                    {
                        table.AddRow(worker.Name, $"{(int)worker.WorkedHours.TotalHours:D2}:{(int)worker.WorkedHours.Minutes:D2}", $"{(int)worker.TotalHours.TotalHours:D2}:{(int)worker.TotalHours.Minutes:D2}");
                    }

                    AnsiConsole.Write(table);
                    Console.WriteLine("Press any key to return...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                UserInterface.RunMenu();
            }
        }

        internal static int CountWorkers()
        {
            int workers = 0;
            try
            {
                var client = new RestClient("http://localhost:5259/");
                var request = new RestRequest($"getall", Method.Get);
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseJson = response.Content;
                    var desserialize = JsonConvert.DeserializeObject<List<Worker>>(responseJson);

                    workers = desserialize.Count();
                }
                return workers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return workers;

            }
            
        }
    }
}
