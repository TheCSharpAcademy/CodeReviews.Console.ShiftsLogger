using ConsoleTableExt;
using System.Text.Json;
using static ShiftLogger.APIClient;

namespace ShiftLogger;

internal class ManagmentInput
{
    static bool updateWorker;
    static bool seeWorker;
    static bool deleteWorker;
    public static bool workInput;
    public static bool workerLoginLogout;

    internal static async Task ManagerUI()
    {
        var apiClient = new APIClient();

        Console.Clear();
        Console.WriteLine("Manager Options:");
        Console.WriteLine("1. List all super heroes");
        Console.WriteLine("2. Get a super hero by ID");
        Console.WriteLine("3. Add a new super hero");
        Console.WriteLine("4. Update an existing super hero");
        Console.WriteLine("5. Delete an existing super hero");
        Console.WriteLine("6. See super hero log into shifts");
        Console.WriteLine("7. Go Back");

        while (true)
        {
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ListSuperHeroesAsync(apiClient);
                    break;

                case "2":
                    seeWorker = true;
                    await ListSuperHeroesAsync(apiClient);
                    await GetSuperHeroAsync(apiClient);
                    break;

                case "3":
                    await AddSuperHeroAsync(apiClient);
                    break;

                case "4":
                    updateWorker = true;
                    await ListSuperHeroesAsync(apiClient);
                    await UpdateSuperHeroAsync(apiClient);
                    break;

                case "5":
                    deleteWorker = true;
                    await ListSuperHeroesAsync(apiClient);
                    await DeleteSuperHeroAsync(apiClient);
                    break;

                case "6":
                    await WorkerInput.SeeWorkerLogIn(apiClient);
                    break;

                case "7":
                    Console.Clear();
                    await MainMenuInput.Main();
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }


    internal static async Task ListSuperHeroesAsync(APIClient apiClient)
    {
        Console.WriteLine("Listing all super heroes...");

        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<SuperHero[]>(superHeroesJson, options).ToList();

        ConsoleTableBuilder.
                From(superHeroes).
                WithFormat(ConsoleTableBuilderFormat.Alternative).
                ExportAndWriteLine(TableAligntment.Center);

        if (!updateWorker && !seeWorker && !workInput && !deleteWorker && !workerLoginLogout)
        {
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();

            await ManagerUI();
        }

        if (workInput)
        {
            workInput = false;
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await WorkerInput.WorkerUI();
        }
    }

    private static async Task GetSuperHeroAsync(APIClient apiClient)
    {
        seeWorker = false;
        Console.WriteLine("Enter the ID of the superhero (or 'b' to go back to Menu):");
        string input = Console.ReadLine();

        if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            await ManagerUI();
        }

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid ID, please try again.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await GetSuperHeroAsync(apiClient);
        }

        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<List<SuperHero>>(superHeroesJson, options);
        var superhero = superHeroes.FirstOrDefault(hero => hero.Id == id);

        if (superhero == null)
        {
            Console.WriteLine("Superhero not found.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await GetSuperHeroAsync(apiClient);
        }

        Console.WriteLine($"Getting superhero with ID {superhero.Id}...");
        var tableData = new List<SuperHero> { superhero };

        ConsoleTableBuilder
        .From(tableData)
        .WithFormat(ConsoleTableBuilderFormat.Alternative)
        .ExportAndWriteLine(TableAligntment.Center);

        Console.WriteLine("Hit Enter to continue");
        Console.ReadLine();
        await ManagerUI();
    }

    private static async Task AddSuperHeroAsync(APIClient apiClient)
    {
        Console.WriteLine("Enter the name of the new super hero:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the first name of the new super hero:");
        var firstName = Console.ReadLine();

        Console.WriteLine("Enter the last name of the new super hero:");
        var lastName = Console.ReadLine();

        Console.WriteLine("Enter the place of the new super hero:");
        var place = Console.ReadLine();

        Console.WriteLine($"Adding new super hero {name}...");
        var newSuperHero = new SuperHero
        {
            Name = name,
            FirstName = firstName,
            LastName = lastName,
            Place = place
        };

        await apiClient.AddSuperHeroAsync
            (newSuperHero.Name,
            newSuperHero.FirstName,
            newSuperHero.LastName,
            newSuperHero.Place
            );

        await PrintLastSuperHeroAsync(apiClient);
        Console.WriteLine("Hit Enter to continue");
        Console.ReadLine();
        await ManagerUI();
    }

    private static async Task PrintLastSuperHeroAsync(APIClient apiClient)
    {
        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<SuperHero[]>(superHeroesJson, options);
        var lastSuperHero = superHeroes[^1];
        var tableData = new List<SuperHero> { lastSuperHero };

        ConsoleTableBuilder
            .From(tableData)
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .ExportAndWriteLine(TableAligntment.Center);
    }

    private static async Task UpdateSuperHeroAsync(APIClient apiClient)
    {
        updateWorker = false;
        Console.WriteLine("Enter the ID of the superhero (or 'b' to go back to Menu):");
        string input = Console.ReadLine();

        if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            await ManagerUI();                               
        }

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid ID, please try again.");
            await UpdateSuperHeroAsync(apiClient);
        }

        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<List<SuperHero>>(superHeroesJson, options);
        var superhero = superHeroes.FirstOrDefault(hero => hero.Id == id);

        if (superhero == null)
        {
            Console.WriteLine("Superhero not found.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await UpdateSuperHeroAsync(apiClient);
        }

        Console.WriteLine("Enter the new name of the super hero or Enter to keep same:");
        var name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            name = superhero.Name;
        }
        Console.WriteLine(name);
        Console.WriteLine("Enter the new first name of the super hero or Enter to keep same:");
        var firstName = Console.ReadLine();

        if (string.IsNullOrEmpty(firstName))
        {
            firstName = superhero.FirstName;
        }

        Console.WriteLine(firstName);
        Console.WriteLine("Enter the new last name of the super hero or Enter to keep same:");
        var lastName = Console.ReadLine();

        if (string.IsNullOrEmpty(lastName))
        {
            lastName = superhero.LastName;
        }
        Console.WriteLine(lastName);
        Console.WriteLine("Enter the new place of the super hero or Enter to keep same:");
        var place = Console.ReadLine();

        if (string.IsNullOrEmpty(place))
        {
            place = superhero.Place;
        }

        Console.WriteLine(place);
        APIClient.UpdateSuperHeroAsync(id, name, firstName, lastName, place);
        Console.WriteLine($"Super hero with name: {name} updated.");
        Console.WriteLine("Hit Enter to continue");
        Console.ReadLine();
        await ManagerUI();
    }

    private static async Task DeleteSuperHeroAsync(APIClient apiClient)
    {
        Console.WriteLine("Enter the ID of the superhero to delete (or 'b' to go back to Menu):");
        string input = Console.ReadLine();

        if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            await ManagerUI();
        }

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid ID, please try again.");
            await DeleteSuperHeroAsync(apiClient);
        }

        var superHeroesJson = await apiClient.GetSuperHeroesAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var superHeroes = JsonSerializer.Deserialize<List<SuperHero>>(superHeroesJson, options);
        var superhero = superHeroes.FirstOrDefault(hero => hero.Id == id);

        if (superhero == null)
        {
            Console.WriteLine("Superhero not found.");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
            await DeleteSuperHeroAsync(apiClient);
        }

        Console.WriteLine($"Deleting super hero: {superhero.Name}");       
        await apiClient.DeleteSuperHeroAsync(id);
        await WorkerInput.DeleteWorkers(apiClient, id);
        Console.WriteLine($"Super hero with name: {superhero.Name} deleted.");
        Console.WriteLine("Hit Enter to continue");
        Console.ReadLine();
        await ManagerUI();
    }
}
