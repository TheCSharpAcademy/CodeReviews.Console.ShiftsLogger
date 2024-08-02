using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Controllers;
using ShiftsLogger.Services;

namespace ShiftsLogger;

public class App {
    private readonly MainMenuController controller;
    public App(MainMenuController mainMenuController)
    {
        controller = mainMenuController; 
    } 
    
    public void Run() {
        while(true) 
        {
            UI.Clear();   
            var choice = UI.MenuSelection("[green]ShiftsLogger[/] Main Menu", [
                "Exit",
                ..MainMenuController.Options,
            ]);

            if (choice == "Exit"){
                break;
            }

            controller.HandleChoice(choice);
        }
    }
}