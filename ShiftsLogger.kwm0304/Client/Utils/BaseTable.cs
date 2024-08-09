using Client.Utils;
using Spectre.Console;

namespace Client.Utils;

public class BaseTable<T>
{
  public string Title { get; set; }
  public List<T> Rows { get; set; }
  public string[] Columns { get; set; }
  private readonly TableMapper<T> tableMapper;
  public BaseTable(string title, List<T> rows)
  {
    Title = title;
    Rows = rows;
    tableMapper = new TableMapper<T>();
    Columns = tableMapper.CerateColumnNames();
  }
  public void Show()
  {
    var table = new Table()
    .Title(Title)
    .Centered()
    .Border(TableBorder.Markdown)
    .BorderStyle(new Style(foreground: Color.DarkCyan, decoration: Decoration.Bold));

    table.AddColumns(Columns);
    foreach (var row in Rows)
    {
      var tableRow = tableMapper.CreateRowValues(row);
      table.AddRow(tableRow);
    }
    AnsiConsole.Write(table);
  }
}