using Spectre.Console;
public static class DisplayMenu
{
    public static MenuEnums.Main MainMenu()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<MenuEnums.Main>()
                .Title("[bold grey]Select option[/]")
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<MenuEnums.Main>())
                .UseConverter( input => { return input switch {
                    MenuEnums.Main.MANAGESHIFT => "Manage shifts",
                    MenuEnums.Main.MANAGEWORKER => "Manage workers",
                    MenuEnums.Main.EXIT => "Exit program",
                    _ => "Error"
                };})
        );
    }

    public static MenuEnums.Shift ShiftsMenu()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<MenuEnums.Shift>()
                .Title("[bold grey]Select option[/]")
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<MenuEnums.Shift>())
                .UseConverter( input => { return input switch {
                    MenuEnums.Shift.CREATESHIFT => "Create shift",
                    MenuEnums.Shift.READSHIFT => "Display shift",
                    MenuEnums.Shift.UPDATESHIFT => "Update shift",
                    MenuEnums.Shift.DELETESHIFT => "Delete shift",
                    MenuEnums.Shift.BACK => "Back to main",
                    _ => "Error"
                };})
        );
    }

    public static MenuEnums.Worker WorkersMenu()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<MenuEnums.Worker>()
                .Title("[bold grey]Select option[/]")
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<MenuEnums.Worker>())
                .UseConverter( input => { return input switch {
                    MenuEnums.Worker.CREATEWORKER => "Create worker",
                    MenuEnums.Worker.READWORKER => "Display worker",
                    MenuEnums.Worker.READWORKERBYID => "Display specific worker",
                    MenuEnums.Worker.UPDATEWORKER => "Update worker",
                    MenuEnums.Worker.DELETEWORKER => "Delete worker",
                    MenuEnums.Worker.BACK => "Back to main",
                    _ => "Error"
                };})
        );
    }
}