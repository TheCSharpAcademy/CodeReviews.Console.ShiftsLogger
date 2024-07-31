using Spectre.Console;

namespace Client.Utils;

public class StringPrompt
{
  public static T GetAndConfirmResponse<T>(string question)
  {
    while (true)
    {
      T answer = AnsiConsole.Ask<T>(question);

      if (answer != null && !string.IsNullOrWhiteSpace(answer.ToString()))
      {
        bool confirm = AnsiConsole.Confirm("Are you sure?");
        if (confirm)
          return answer;
      }
      else
      {
        AnsiConsole.MarkupLine("[bold red]Response cannot be empty.[/]");
        AnsiConsole.WriteLine("Please enter a valid response:");
      }
    }
  }
}