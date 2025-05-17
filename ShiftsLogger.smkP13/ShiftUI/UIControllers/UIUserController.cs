using Newtonsoft.Json;
using RestSharp;
using ShiftUI.UIModels;
using Spectre.Console;

namespace ShiftUI.UIControllers;

class UIUserController
{
    public static List<UIUser>? GetAllUsers()
    {
        using RestClient client = new("https://localhost:7029/");
        RestRequest request = new("api/User");
        request.AlwaysMultipartFormData = true;
        RestResponse response = client.GetAsync(request).Result;
        List<UIUser>? users = [];
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Content;
            if (rawResponse != null)
            {
                users = JsonConvert.DeserializeObject<List<UIUser>>(rawResponse);
                users?.OrderBy(x => x.userId);
            }
        }
        return users;
    }

    internal static void CreateNewUser(UIUser user)
    {
        using RestClient client = new("https://localhost:7029/");
        RestRequest request = new("api/User");
        request.AddBody(user);
        RestResponse response = client.PostAsync(request).Result;
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            AnsiConsole.MarkupLine("New user successfully[green] added[/].");
            return;
        }
        AnsiConsole.MarkupLine("[red]New user was not added.[/] Verify data format.");
    }

    internal static void DeleteUser(int userId)
    {
        using RestClient client = new("https://localhost:7029/");
        RestRequest request = new($"api/User/{userId}");
        RestResponse response = client.DeleteAsync(request).Result;
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            AnsiConsole.MarkupLine("New user successfully [green]deleted[/].");
            return;
        }
        AnsiConsole.MarkupLine("[red]New user was not deleted.[/] Verify data format.");
    }

    internal static void UpdateUser(UIUser user)
    {
        using RestClient client = new("https://localhost:7029/");
        RestRequest request = new($"api/User/{user.userId}");
        request.AddBody(user);
        RestResponse response = client.PutAsync(request).Result;
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            AnsiConsole.MarkupLine("New user successfully [green]updated[/].");
            return;
        }
        AnsiConsole.MarkupLine("[red]New user was not updated.[/] Verify data format.");
    }
}