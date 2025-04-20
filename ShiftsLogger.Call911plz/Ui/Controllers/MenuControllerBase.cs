
using System.Threading.Tasks;
using Spectre.Console;

public class MenuControllerBase
{
    internal virtual void OnMake() {}
    internal virtual void OnReady() { Console.Clear(); }
    internal virtual async Task<bool> HandleMenuSelectionAsync() { return await Task.FromResult(true); }
    internal virtual void OnExit() {}
    public async Task StartAsync()
    {
        OnMake();
        
        bool exit = false;
        while (exit == false)
        {
            OnReady();
            exit = await HandleMenuSelectionAsync();

            if (exit != true)
            {
                AnsiConsole.MarkupLine("[bold yellow]Press Enter to continue[/]");
                Console.Read();
            }
        }

        OnExit();
    }
}