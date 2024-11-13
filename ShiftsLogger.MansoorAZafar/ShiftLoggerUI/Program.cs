using ShiftLoggerUI.Views;
using ShiftLoggerUI.Controllers;
using ShiftLoggerUI.Models;

//Initialize Variables
ShiftManager shiftManager = new();

//Get the Selection
ShiftSelection selection = ShiftSelection.Exit;
do {
    System.Console.Clear();
    SpectreDisplay.DisplayFiglet("Shift Logger!", "center");
    SpectreDisplay.DisplayHeader("Home", "left");

    selection = Utilities.GetSelection<ShiftSelection>
                (
                    Enum.GetValues<ShiftSelection>(),
                    "Enter your Selection:",
                    alternateNames: item => item switch 
                    {
                        ShiftSelection.AddShift => "Add a Shift",
                        ShiftSelection.GetShift => "Get the Shifts",
                        ShiftSelection.UpdateShift => "Update the Shifts",
                        ShiftSelection.RemoveShift => "Remove a Shift",
                        _ => "Exit"
                    }
                );
    
    shiftManager.HandleShiftSelection(selection);
    
} while(selection != ShiftSelection.Exit);

System.Console.Clear();
SpectreDisplay.DisplayFiglet("Goodbye!");