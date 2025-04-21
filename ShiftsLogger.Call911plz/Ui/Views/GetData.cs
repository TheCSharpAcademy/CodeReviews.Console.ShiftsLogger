using Spectre.Console;

public class GetData
{
    public static WorkerDto GetWorker(Worker? existingWorker = null)
    {
        existingWorker ??= new Worker();
        return new WorkerDto()
        {
            WorkerName = GetWorkerName(existingWorker.WorkerName),
            WorkerId = GetWorkerId(existingWorker.WorkerId)
        };
    }

    // The get functions will add in default values for user convience if an existing 
    // data is inputed.
    private static string GetWorkerName(string existingName = "")
    {
        TextPrompt<string> prompt = new("[bold grey]Enter worker name:[/]");

        if (existingName != string.Empty)
            prompt.DefaultValue(existingName);

        return AnsiConsole.Prompt(prompt);
    }

    private static int GetWorkerId(int existingId = default)
    {
        TextPrompt<int> prompt = new("[bold grey]Enter worker id:[/]");

        if (existingId != default)
            prompt.DefaultValue(existingId);
        
        return AnsiConsole.Prompt(prompt);
    }

    public static ShiftDto GetShift(Shift? existingShift = null)
    {
        existingShift ??= new Shift();
        return new ShiftDto()
        {
            StartDateTime = GetDateTime("[bold grey]Enter start date[/]", existingShift.StartDateTime),
            EndDateTime = GetDateTime("[bold grey]Enter end date[/]", existingShift.StartDateTime),
        };
    }

    private static DateTime GetDateTime(string stringPrompt, DateTime existingStartTime = default)
    {
        TextPrompt<string> prompt = new(stringPrompt);

        if (existingStartTime != default)
            prompt.DefaultValue(existingStartTime.ToString());
        else
            prompt.DefaultValue(DateTime.Now.ToString());
        
        prompt.Validate(input => 
            {
                if (DateTime.TryParse(input, out _))
                    return ValidationResult.Success();
                return ValidationResult.Error("[bold red]Invalid date[/]");
            }
        );

        _ = DateTime.TryParse(AnsiConsole.Prompt(prompt), out var output);
        return output;
    }

    public static int FindWorker(List<Worker> workers)
    {
        TextPrompt<int> prompt = new("[bold grey]Enter worker id:[/]");
        prompt.Validate(input => 
        {
            if (workers.Find(worker => worker.WorkerId == input) != null)
                return ValidationResult.Success();
            return ValidationResult.Error("[bold red]Could not find given id[/]");
        });
        
        return AnsiConsole.Prompt(prompt);
    }
}