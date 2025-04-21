
using Spectre.Console;

public class MenuControllerBase
{
    internal virtual void OnMake() {}
    internal virtual Task OnMakeAsync() { return Task.CompletedTask; }
    internal virtual void OnReady() { Console.Clear(); }
    internal virtual async Task<bool> HandleMenuSelectionAsync() { return await Task.FromResult(true); }
    internal virtual void OnExit() {}
    public async Task StartAsync()
    {
        try
        {
            OnMake();
            await OnMakeAsync();
        }
        catch (Exception e) 
        {
            AnsiConsole.MarkupLine($"[bold red]Error on make: [/]{e.Message}"); 
            AnsiConsole.MarkupLine($"[bold yellow]Press Enter to continue: [/]"); 
            Console.Read();
        }
        
        bool exit = false;
        while (exit == false)
        {
            try
            {
                OnReady();
                exit = await HandleMenuSelectionAsync();

                if (exit != true)
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press Enter to continue[/]");
                    Console.Read();
                }
            }
            catch (Exception e) 
            { 
                AnsiConsole.MarkupLine($"[bold red]Error in loop: [/]{e.Message}"); 
                AnsiConsole.MarkupLine($"[bold yellow]Press Enter to continue: [/]"); 
                Console.Read();
            }
                
        }

        try 
        {
            OnExit();
        }
        catch (Exception e) 
        { 
            AnsiConsole.MarkupLine($"[bold red]Error on exit: [/]{e.Message}");
            AnsiConsole.MarkupLine($"[bold yellow]Press Enter to continue: [/]"); 
            Console.Read();    
        }
    }
}