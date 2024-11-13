using Spectre.Console;
using RestSharp;
using Microsoft.Extensions.Configuration;
using ShiftLoggerUI.Models;
using System.Runtime.CompilerServices;

namespace ShiftLoggerUI.Controllers;
internal static class Utilities
{
    internal static readonly string API_URL;
    internal static readonly RestClient client;

    static Utilities()
    {
        API_URL = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", false, false)
                      .Build()["API_HOST"] ?? "n/a";

        client = new RestClient(API_URL);
    }

    internal static T GetSelection<T>(T[] selections, 
                                        string prompt, 
                                        Func<T, string>? alternateNames = null
                                    ) where T: notnull
    => AnsiConsole.Prompt(
            new SelectionPrompt<T>()
            .Title(prompt)
            .PageSize(15)
            .AddChoices(selections)
            .UseConverter(item => alternateNames != null 
                          ? alternateNames(item) : item.ToString() 
                          ?? string.Empty)
        );

    
    internal static int GetShiftID()
    {
        System.Console.Write("Enter the ID or Enter -1 To Exit\n> ");
        //Make request to Site & Check if ID Exists
        int id;
        while(!Int32.TryParse(System.Console.ReadLine(), out id) || (id != -1 && !ShiftExists(id)))
            System.Console.Write("Please Enter a valid ID\nOr enter -1 to Exit\n> ");

        return id;
    }

    private static bool ShiftExists(int id)
    {   
        try {
            RestRequest request = new($"{id}");
            var response = client.ExecuteGet(request);
            
            if(response.IsSuccessStatusCode)
                return true;

        } catch(Exception e) {
            System.Console.WriteLine(e.Message);
        }
        
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void PressToContinue()
    {
        System.Console.WriteLine("\nPress Any Key To Continue...");
        System.Console.ReadKey();
    }

    internal static Shift CreateShift()
    {
        string employeeName = AnsiConsole.Prompt<string>(
                new TextPrompt<string>("Enter the Employee's Name\n> "));

        double startTime = Math.Round(AnsiConsole.Prompt<double>(
            new TextPrompt<double>("Enter the Starting Time (24 Decimal Hours System, Ex. 7.25 -> 7:15 am)\n> ")
            .Validate((n) => n switch 
            {
                < 0 => ValidationResult.Error("Too Low, (0-24)"),
                > 24 => ValidationResult.Error("Too High, (0-24)"),
                _ => ValidationResult.Success()
            })), 2);

        double endTime = -1;
        do 
        {
            endTime = AnsiConsole.Prompt<double>(
            new TextPrompt<double>("Enter the End Time (24 Decimal Hours System, Ex. 22.80 = 10:45 pm)\n> ")
            .Validate((n) => n switch 
            {
                < 0 => ValidationResult.Error("Too Low, (0-24)"),
                > 24 => ValidationResult.Error("Too High, (0-24)"),
                _ => ValidationResult.Success()
            }));
        }while(endTime <= startTime);

        DateOnly date = AnsiConsole.Prompt<DateOnly>(
            new TextPrompt<DateOnly>("Enter The Date (yyyy-mm-dd)\n> "));

        return new Shift(employeeName, startTime, endTime, date);
    }


}