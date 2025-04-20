
using System.Threading.Tasks;
using Spectre.Console;

public class MenuControllerBase
{
    internal virtual void OnMake() {}
    internal virtual void OnReady() { Console.Clear(); }
    internal virtual async Task<bool> HandleMenuSelection() { return await Task.FromResult(true); }
    internal virtual void OnExit() {}
    public async Task Start()
    {
        OnMake();
        
        bool exit = false;
        while (exit == false)
        {
            OnReady();
            exit = await HandleMenuSelection();

            if (exit != true)
            {
                AnsiConsole.MarkupLine("[bold yellow]Press Enter to continue[/]");
                Console.Read();
            }
        }

        OnExit();
    }
}