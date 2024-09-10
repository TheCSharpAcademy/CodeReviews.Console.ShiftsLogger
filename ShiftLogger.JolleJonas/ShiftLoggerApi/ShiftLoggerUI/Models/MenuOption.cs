namespace ShiftLoggerUI.Models;

public class MenuOption
{
    public string Name { get; set; }
    public Func<Task> Action { get; set; }

    public MenuOption(string name, Func<Task> action)
    {
        Name = name;
        Action = action;
    }
}
