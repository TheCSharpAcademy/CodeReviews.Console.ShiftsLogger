﻿using ConsoleTableExt;
using ShiftApi.DTOs;
using ShiftApi.Models;
using ShiftUI.Controllers;
using Spectre.Console;

namespace ShiftUI;

internal class VisualizationEngine
{

    internal static async Task MainMenu()
    {
        var endApplication = false;

        while (!endApplication)
        {
            Console.Clear();

            var mainMenuChoice = UserInput.GetMainMenuChoice();

            switch (mainMenuChoice)
            {
                case "Current employee":
                    try
                    {
                        if (await Helper.EmployeesExist()) await VisualizationEngine.CurrentEmployeeMenu();
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"Unexpected error: {ex.Message}\nPress any key to continue.");
                        Console.ReadKey();
                    }
                    break;
                case "New employee":
                    try
                    {
                        await ApiService.CreateEmployee();
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"Unexpected error: {ex.Message}\nPress any key to continue.");
                        Console.ReadKey();
                    }
                    break;
                case "Update employee":
                    try
                    {
                        if (await Helper.EmployeesExist())
                        {
                            var employeeToUpdate = await Helper.GetEmployeeToUpdate();
                            await ApiService.UpdateEmployee(employeeToUpdate);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"Unexpected error: {ex.Message}\nPress any key to continue.");
                        Console.ReadKey();
                    }
                    break;
                case "Delete employee":
                    try
                    {
                        if (await Helper.EmployeesExist())
                        {
                            var employeeIdToBeDeleted = await Helper.GetIdOfEmployeeToDelete();
                            var confirmation = AnsiConsole.Confirm("Are you sure you want to delete this employee?");
                            if (confirmation) await ApiService.DeleteEmployee(employeeIdToBeDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"Unexpected error: {ex.Message}\nPress any key to continue.");
                        Console.ReadKey();
                    }
                    break;
                case "Quit application":
                    endApplication = true;
                    break;
            }

        }
    }

    private static async Task CurrentEmployeeMenu()
    {
        Console.Clear();

        var employees = await ApiService.GetEmployees();

        var employeeId = UserInput.GetEmployeeChoiceById(employees);

        var employee = await ApiService.GetEmployee(employeeId);

        await VisualizationEngine.EmployeeShiftLoggerMenu(employee);

    }

    private static async Task EmployeeShiftLoggerMenu(Employee employee)
    {
        var exitEmployeeShiftLoggerMenu = false;

        while (!exitEmployeeShiftLoggerMenu)
        {
            employee = await ApiService.GetEmployee(employee.EmployeeId);
            Console.Clear();

            var menuChoice = UserInput.GetEmployeeShiftLoggerMenuChoice(employee);

            switch (menuChoice)
            {
                case "Add shift":
                    var shift = ApiService.CreateShift(employee);
                    if (shift is null) continue;
                    await ApiService.AddShift(employee, shift);
                    break;
                case "View shifts":
                    var employeeShifts = await ApiService.GetEmployeeShifts(employee);
                    VisualizationEngine.DisplayEmployeeShifts(employeeShifts, employee.FirstName, employee.LastName);
                    break;
                case "Update shift":
                    if (employee.Shifts.Count == 0)
                    {
                        await Console.Out.WriteLineAsync("\nNo shifts to update, press Enter to continue.");
                        Console.ReadLine();
                        continue;
                    }
                    var shiftToUpdateId = await ApiService.GetShiftId(employee);
                    var shiftToUpdate = await ApiService.GetShift(shiftToUpdateId);
                    await ApiService.UpdateShift(shiftToUpdate);
                    break;
                case "Delete shift":

                    if (employee.Shifts.Count == 0)
                    {
                        await Console.Out.WriteLineAsync("\nNo shifts to delete, press Enter to continue.");
                        Console.ReadLine();
                        continue;
                    }

                    var shiftToDeleteId = await ApiService.GetShiftId(employee);
                    var shiftToDelete = await ApiService.GetShift(shiftToDeleteId);
                    await ApiService.DeleteShift(shiftToDelete);
                    break;
                case "Return to main menu":
                    exitEmployeeShiftLoggerMenu = true;
                    break;
            }
        }
    }

    private static void DisplayEmployeeShifts(List<ShiftDTO> employeeShifts, string firstName, string lastName)
    {
        Console.Clear();
        ConsoleTableBuilder.From(employeeShifts)
            .WithColumn("Shift ID", "Start Date & Time", "End Date & Time")
            .ExportAndWriteLine(TableAligntment.Center);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}
