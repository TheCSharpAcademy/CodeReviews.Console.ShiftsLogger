﻿using ShiftsLogger.frockett.UI.UI;
using Spectre.Console;
using ShiftsLogger.frockett.UI.Dtos;
using System.Runtime.CompilerServices;

namespace ShiftsLogger.frockett.UI;

public class Menu
{
    private readonly ApiService apiService;
    private readonly TableEngine tableEngine;
    public Menu(ApiService apiService, TableEngine tableEngine)
    {
        this.apiService = apiService;
        this.tableEngine = tableEngine;
    }

    public async Task MainMenuHandler()
    {
        //var mainMenuOptions = Enum.GetValues(typeof(MainMenuOptions)).Cast<MainMenuOptions>().ToArray();

        var menuSelection = new SelectionPrompt<MainMenuOptions>()
            .Title("Main Menu")
            .AddChoices(Enum.GetValues<MainMenuOptions>())
            .UseConverter(option => option.GetEnumDescription());
        /*
        menuSelection.AddChoices(MainMenuOptions.ViewShifts, MainMenuOptions.ViewShifts,
                                MainMenuOptions.ViewEmployeeShifts, MainMenuOptions.AddShift, 
                                MainMenuOptions.DeleteShift, MainMenuOptions.UpdateShift,
                                MainMenuOptions.AddEmployee, MainMenuOptions.DeleteEmployee,
                                MainMenuOptions.UpdateEmployee, MainMenuOptions.Exit);
        */
        var selection = AnsiConsole.Prompt(menuSelection);

        switch (selection)
        {
            case MainMenuOptions.ViewShifts:
                await HandleViewShifts();
                break;
            case MainMenuOptions.ViewEmployeeShifts:
                HandleViewEmployeeShifts();
                break;
            case MainMenuOptions.AddShift:
                HandleAddShift();
                break;
            case MainMenuOptions.DeleteShift:
                HandleDeleteShifts();
                break;
            case MainMenuOptions.UpdateShift:
                HandleUpdateShift();
                break;
            case MainMenuOptions.AddEmployee:
                HandleAddEmplyee();
                break;
            case MainMenuOptions.DeleteEmployee:
                HandleDeleteEmployee();
                break;
            case MainMenuOptions.UpdateEmployee:
                HandleUpdateEmployee();
                break;
            case MainMenuOptions.Exit:
                Environment.Exit(0);
                break;
        }
    }

    private async Task HandleViewShifts()
    {
        var shiftDtos = await apiService.GetAllShifts();
        tableEngine.PrintShifts(shiftDtos);
        PauseForUser();
        await MainMenuHandler();
    }

    private void PauseForUser()
    {
        AnsiConsole.WriteLine("Press Enter to Continue...");
        Console.ReadLine();
        Console.Clear();
    }

    private void HandleViewEmployeeShifts()
    {
        throw new NotImplementedException();
    }

    private void HandleAddShift()
    {
        throw new NotImplementedException();
    }

    private void HandleDeleteShifts()
    {
        throw new NotImplementedException();
    }

    private void HandleUpdateShift()
    {
        throw new NotImplementedException();
    }

    private void HandleAddEmplyee()
    {
        throw new NotImplementedException();
    }

    private void HandleDeleteEmployee()
    {
        throw new NotImplementedException();
    }

    private void HandleUpdateEmployee()
    {
        throw new NotImplementedException();
    }

    private void HandleDisplayEmployees()
    {
        throw new NotImplementedException();
    }
}