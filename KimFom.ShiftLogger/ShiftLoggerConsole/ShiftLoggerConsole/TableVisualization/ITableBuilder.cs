using ShiftLoggerConsole.Models;

namespace ShiftLoggerConsole.TableVisualization;

public interface ITableBuilder
{
    public void DisplayTable(List<Shift>? shifts);
}