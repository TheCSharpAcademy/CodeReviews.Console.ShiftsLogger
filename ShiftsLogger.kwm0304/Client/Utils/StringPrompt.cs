using Spectre.Console;

namespace Client.Views;

public class StringPrompt
{
  public static string GetAndConfirmResponse(string question)
  {
    string answer = AnsiConsole.Ask<string>(question);
    if (!string.IsNullOrEmpty(answer) ||
      !string.IsNullOrWhiteSpace(answer))
    {
      bool confirm = AnsiConsole.Confirm("Are you sure?");
      if (confirm)
        return answer;
      else
        return GetAndConfirmResponse(question);
    }
    AnsiConsole.MarkupLine("[bold red]Response cannot be empty.[/]\n Please enter a valid response:");
    return GetAndConfirmResponse(question);
  }
}
