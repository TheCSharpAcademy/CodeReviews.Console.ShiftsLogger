using System.Threading.Channels;
using ShiftLogger.Console_App.Models;
using Spectre.Console;

namespace ShiftLogger.Console_App;

public class Application
{
    private UserInput _userInput;
    private ShiftService _service;
    public Application(UserInput userInput, ShiftService service)
    {
        _userInput = userInput;
        _service = service;
    }
    public async Task Build()
    {
        bool exitApp = false;
        while (!exitApp)
        {
            Console.Clear();
            View.RenderTitle("Shift - Logger");
            var selected = _userInput.ChooseMenuOperation();

            switch(selected)
            {
                case "Get All Shifts":
                    Console.Clear();
                    var listOfShift = await _service.GetAllShiftAsync();
                    View.RenderTable(listOfShift, Color.Aqua);
                    AnsiConsole.MarkupLine("[grey bold](Press 'Enter' to continue.)[/]");
                    Console.ReadLine();
                    break;

                case "Create Shift":
                    var shift = _userInput.CreateShift();
                    if(shift == null) break;
                    
                    var createdShift = await _service.CreateShiftAsync(shift);
                    Console.WriteLine();
                    if(createdShift.Id > 0)
                    {
                        View.RenderResult($"Record was created with Id: {createdShift.Id}", "green", Color.Aquamarine3);
                        View.RenderTable(new List<Shift>() {createdShift}, Color.Aqua);
                    }
                    else
                    {
                        View.RenderResult("UNABLE TO CREATE", "red", Color.Red3);
                    }
                    AnsiConsole.MarkupLine("[grey bold](Press 'Enter' to continue.)[/]");
                    Console.ReadLine();
                    break;
                
                case "Update Shift":
                    Console.Clear();
                    listOfShift = await _service.GetAllShiftAsync();
                    View.RenderTable(listOfShift, Color.Yellow1);

                    if(listOfShift.Count() == 0) break;
                    AnsiConsole.Write(new Rule("[blue3 bold]CHOOSE THE ID FROM THE ABOVE TO UPDATE[/]").RuleStyle("aqua"));

                    var updatedShift = _userInput.UpdateShift(listOfShift.ToList());
                    if(updatedShift == null) break;

                    updatedShift = await _service.UpdateShiftAsync(updatedShift);
                    Console.WriteLine();
                    if(updatedShift.Id > 0)
                    {
                        View.RenderResult($"Record was updated with Id: {updatedShift.Id}", "yellow", Color.Yellow3_1);
                        View.RenderTable(new List<Shift>() {updatedShift}, Color.Yellow4_1);
                    }
                    else
                    {
                        View.RenderResult("UNABLE TO CREATE", "red", Color.Red3);
                    }
                    AnsiConsole.MarkupLine("[grey bold](Press 'Enter' to continue.)[/]");
                    Console.ReadLine();

                    break;

                case "Delete Shift":
                    Console.Clear();
                    listOfShift = await _service.GetAllShiftAsync();
                    View.RenderTable(listOfShift, Color.Red3);

                    if(listOfShift.Count() == 0) break;
                    AnsiConsole.Write(new Rule("[blue3 bold]CHOOSE THE ID FROM THE ABOVE TO DELETE[/]").RuleStyle("red"));

                    var deleteId = _userInput.DeleteId(listOfShift.ToList());
                    if(deleteId == 0) break;

                    int deletedId = await _service.DeleteIdAsync(deleteId);

                    Console.WriteLine();
                    if(deletedId > 0)
                    {
                        View.RenderResult($"Record was Deleted with Id: {deletedId}", "red", Color.OrangeRed1);
                        View.RenderTable(new List<Shift>() {listOfShift.First(s => s.Id == deletedId)}, Color.Red3);
                    }
                    else
                    {
                        View.RenderResult("UNABLE TO DELETE", "red", Color.Red3);
                    }
                    AnsiConsole.MarkupLine("[grey bold](Press 'Enter' to continue.)[/]");
                    Console.ReadLine();

                    break;

                case "Exit":
                    Console.Clear();
                    View.RenderTitle("Have a Nice day!!");
                    exitApp = true;
                    break;
            }

        }

    }
}
