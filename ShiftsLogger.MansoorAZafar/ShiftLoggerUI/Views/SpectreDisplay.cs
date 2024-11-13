using Spectre.Console;

namespace ShiftLoggerUI.Views;
internal static class SpectreDisplay
{
    internal static void DisplayFiglet(string text, string justify = "left")
    {
        FigletText prompt = 
            new FigletText(text)
            .Color(Color.Red)
            .Justify(justify switch {
                "left" => Justify.Left,
                "right" => Justify.Right,
                _ => Justify.Center
            });

        AnsiConsole.Write(prompt);
    }

    internal static void DisplayHeader(string header, string justify = "left")
    {
        Rule heading = new ($"[red]{header}[/]");   
        heading.Justification = justify switch {
            "left" => Justify.Left,
            "right" => Justify.Right,
            _ => Justify.Center
        };
        AnsiConsole.Write(heading);
        System.Console.WriteLine();
    }

    internal static void DisplayTable<T>(string[] headers, List<T> data)
    {
        Table table = new();
        
        //Add the headers
        foreach(string header in headers)
            table.AddColumns(header);

        // Loop through every item
        foreach(T item in data)
        {
            List<string> row = new();

            //Match it with the actual property 
            foreach(string header in headers)
            {
                System.Reflection.PropertyInfo? property = typeof(T).GetProperty(header);
                row.Add(property.GetValue(item).ToString() ?? "N/A");
            }

            table.AddRow(row.ToArray());
        }

        AnsiConsole.Write(table);
        System.Console.WriteLine();
    }
}