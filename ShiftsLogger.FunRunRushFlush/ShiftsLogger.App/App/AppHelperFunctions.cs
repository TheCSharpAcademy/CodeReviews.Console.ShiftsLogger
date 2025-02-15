using Spectre.Console;

namespace ShiftsLogger.App.App;


public static class AppHelperFunctions
{
    public static void AppHeader(string headerTitel, bool link = false)
    {
        AnsiConsole.Write(new FigletText(headerTitel).Centered().Color(Color.Blue));
        
        if (link)
        {
            AnsiConsole.Write(
             new Markup("[blue]Inspired by the [link=https://thecsharpacademy.com/project/17/shifts-logger]C#Academy[/][/]")
             .Centered());
        }
        AnsiConsole.MarkupLine("");
    }

    public static bool ReturnMenu()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<bool>($"[yellow]You want to go back?[/]")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(false)
            .WithConverter(choice => choice ? "y" : "n"));
    }
    public static bool CloseApp()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<bool>($"[yellow]Are you sure you want to Close the App[/]")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(false)
            .WithConverter(choice => choice ? "y" : "n"));
    }




}
