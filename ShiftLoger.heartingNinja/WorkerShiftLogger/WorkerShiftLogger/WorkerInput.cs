using ConsoleTableExt;
using System.Text.Json;
using static ShiftLogger.ApiClient;

namespace ShiftLogger;

internal class WorkerInput
{
    internal static async Task WorkerUI()
    {
        var apiClient = new ApiClient();

        Console.Clear();
        Console.WriteLine("Worker Options:");
        Console.WriteLine("1. Clock In");
        Console.WriteLine("2. Clock Out");
        Console.WriteLine("3. List Workers with name");
        Console.WriteLine("4. Go Back");

        while (true)
        {
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ManagmentInput.workerLoginLogout = true;
                    await ManagmentInput.ListSuperHeroesAsync(apiClient);
                    await LogIn(apiClient);
                    break;

                case "2":
                    ManagmentInput.workerLoginLogout = true;
                    await ManagmentInput.ListSuperHeroesAsync(apiClient);
                    await LogOut(apiClient);
                    break;

                case "3":
                    ManagmentInput.workInput = true;
                    await ManagmentInput.ListSuperHeroesAsync(apiClient);
                    break;

                case "4":
                    Console.Clear();
                    await MainMenuInput.Main();
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
   
    private static async Task LogIn(ApiClient apiClient)
    {
        ManagmentInput.workerLoginLogout = false;
        Console.WriteLine("Enter your worker ID log in (or 'b' to go back):");
        string input = Console.ReadLine();

        if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            await WorkerUI();
        }

        int superheroId;
        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<List<SuperHero>>(superHeroesJson, options);

        if (!int.TryParse(input, out superheroId))
        {

            Console.WriteLine("Invalid worker ID log in, please try again.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await LogIn(apiClient);
        }

        var superhero = superHeroes.FirstOrDefault(hero => hero.Id == superheroId);

        if (superhero == null)
        {
            Console.WriteLine($"Superhero with ID {superheroId} not found.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await LogIn(apiClient);
        }
        else
        {         
            var workersJson = await apiClient.GetWorkersAsync();
            var workers = JsonSerializer.Deserialize<WorkerShift[]>(workersJson, options);
            var loggedInWorker = workers.FirstOrDefault(w => w.SuperHeroId == superhero.Id && w.LogoutTime == DateTime.MinValue);

            if (loggedInWorker != null)
            {
                Console.WriteLine($"{superhero.Name} is already logged in.");
                Console.WriteLine("Hit Enter to continue");
                Console.ReadLine();
                Console.Clear();
                await WorkerUI();
            }

            DateTime loginTime = DateTime.Now;
            DateTime logoutTime = DateTime.MinValue;

            await apiClient.AddWorkerAsync(superhero.Id, superhero.Name, loginTime, logoutTime);
            Console.WriteLine($"{superhero.Name} is now logged in at {loginTime}.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await WorkerUI();
        }
    }

    private static async Task LogOut(ApiClient apiClient)
    {
        ManagmentInput.workerLoginLogout = false;
        Console.WriteLine("Enter your worker ID log out (or 'b' to go back):");

        string input = Console.ReadLine();
        int superheroId;
        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<List<SuperHero>>(superHeroesJson, options);

        if (!int.TryParse(input, out superheroId))
        {
            if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                await WorkerUI();
            }
            else
            {
                Console.WriteLine("Invalid worker ID log out, please try again.");
                Console.WriteLine("Hit Enter to continue");
                Console.ReadLine();
                await LogIn(apiClient);
            }
        }

        var superhero = superHeroes.FirstOrDefault(hero => hero.Id == superheroId);

        if (superhero == null)
        {
            Console.WriteLine($"Superhero with ID {superheroId} not found.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await LogIn(apiClient);
        }
     
        var response = await apiClient.GetSuperHeroAsyncID(superheroId);      
        var superHero = JsonSerializer.Deserialize<SuperHero>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var workerResponse = await apiClient.GetWorkerAsync(superHero?.Id ?? 0);

        if (!string.IsNullOrEmpty(workerResponse))
        {
            var worker = JsonSerializer.Deserialize<WorkerShift>(workerResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (worker.LogoutTime == DateTime.MinValue)
            {
                worker.LogoutTime = DateTime.Now;

                await apiClient.UpdateWorkerAsync(
                    worker.Id,
                    worker.SuperHeroId,
                    worker.Name,
                    worker.LoginTime,
                    DateTime.Now);

                Console.WriteLine($"{superHero.Name} is now logged out at {worker.LogoutTime}.");
            }
            else
            {
                Console.WriteLine("No worker found with matching LoginTime or worker has already logged out.");
            }
        }
        else
        {
            Console.WriteLine("No worker found for the selected superhero.");
        }

        Console.WriteLine("Hit Enter to continue");
        Console.ReadLine();
        await WorkerUI();
    }

    internal static async Task SeeWorkerLogIn(ApiClient apiClient)
    {

        Console.WriteLine("List of Workers:");
        Console.WriteLine("Listing all shifts...");

        var workers = await apiClient.GetWorkersAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var workerShifts = JsonSerializer.Deserialize<WorkerShift[]>(workers, options).ToList();

        ConsoleTableBuilder.
                From(workerShifts).
                WithFormat(ConsoleTableBuilderFormat.Alternative).
                ExportAndWriteLine(TableAligntment.Center);

        Console.WriteLine("Hit Enter to continue");
        Console.ReadLine();
        await ManagmentInput.ManagerUI();
    }

    internal static async Task DeleteWorkers(ApiClient apiClient, int workerId)
    {
        await apiClient.DeleteWorkerAsync(workerId);
    }
}