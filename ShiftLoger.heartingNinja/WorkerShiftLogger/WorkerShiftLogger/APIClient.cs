using System.Net.Http.Json;

namespace ShiftLogger;

public class ApiClient
{
    private static readonly HttpClient httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:7291")
    };

    public async Task<string> GetSuperHeroesAsync()
    {
        var response = await httpClient.GetAsync("/api/SuperHero");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> GetSuperHeroAsyncID(int id)
    {
        var response = await httpClient.GetAsync($"/api/SuperHero/{id}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> AddSuperHeroAsync(string name, string firstName, string lastName, string place)
    {
        var hero = new SuperHero { Name = name, FirstName = firstName, LastName = lastName, Place = place };
        var response = await httpClient.PostAsJsonAsync("/api/SuperHero", hero);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public static async Task<string> UpdateSuperHeroAsync(int id, string name, string firstName, string lastName, string place)
    {
        var hero = new SuperHero { Id = id, Name = name, FirstName = firstName, LastName = lastName, Place = place };
        var url = $"/api/SuperHero/{id}";
        var response = await httpClient.PutAsJsonAsync(url, hero);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> DeleteSuperHeroAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/SuperHero/{id}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> GetWorkersAsync()
    {
        var response = await httpClient.GetAsync("/api/Worker");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> GetWorkerAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/Worker/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }     
        else
        {       
            response.EnsureSuccessStatusCode();
            return null;
        }
    }

    public async Task<string> AddWorkerAsync(int superHeroId, string name, DateTime loginTime, DateTime logoutTime)
    {
        var worker = new WorkerShift { SuperHeroId = superHeroId, Name = name, LoginTime = loginTime, LogoutTime = logoutTime };
        var response = await httpClient.PostAsJsonAsync("/api/Worker", worker);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> UpdateWorkerAsync(int id, int superHeroId, string name, DateTime loginTime, DateTime logoutTime)
    {
        var worker = new WorkerShift { Id = id, SuperHeroId = superHeroId, Name = name, LoginTime = loginTime, LogoutTime = logoutTime };
        var response = await httpClient.PutAsJsonAsync("/api/Worker", worker);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();

        return result;
    }  

    public async Task<string> DeleteWorkerAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/Worker/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }          
        else
        {          
            response.EnsureSuccessStatusCode();
            return null;
        }
    }

    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Place { get; set; }
    }

    public class WorkerShift
    {
        public int Id { get; set; }
        public int SuperHeroId { get; set; }
        public string Name { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }

    }
}
