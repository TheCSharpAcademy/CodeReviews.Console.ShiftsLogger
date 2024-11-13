using ShiftLoggerUI.Models;
using RestSharp;
using ShiftLoggerUI.Views;
using Newtonsoft.Json;

namespace ShiftLoggerUI.Controllers;

internal class ShiftManager
{
    internal void HandleShiftSelection(ShiftSelection selection)
    {
        System.Console.Clear();
        switch(selection)
        {
            case ShiftSelection.RemoveShift:
                this.RemoveShift();
                break;
            case ShiftSelection.AddShift:
                this.AddShift();
                break;
            case ShiftSelection.UpdateShift:
                this.UpdateShift();
                break;
            case ShiftSelection.GetShift:
                this.GetShifts();
                break;
        }
    }

    internal void GetShifts()
    {
        SpectreDisplay.DisplayHeader("Get Shifts");

        GetShiftSelection selection = Utilities.GetSelection<GetShiftSelection>
            (
                Enum.GetValues<GetShiftSelection>(),
                "Choose an Option",
                alternateNames: item => item switch {
                    GetShiftSelection.All => "Get All Shifts",
                    _ => "Get One Shift By ID"
                }
            );

        int shiftID = -1;

        if (selection == GetShiftSelection.SelectByID)
        {
            shiftID = Utilities.GetShiftID();
            if (shiftID == -1) return; 
        }

        this.DisplayShifts(shiftID);
        Utilities.PressToContinue();
    }

    private void DisplayShifts(int shiftID=-1)
    {
        try
        {
            RestRequest request;
            if (shiftID != -1)
                request = new($"{shiftID}");
            else
                request = new("");

            var response = Utilities.client.ExecuteGet(request);
            List<Shift> shifts = new();

            if (response != null && response.IsSuccessStatusCode) {
                string rawResponse = response.Content ?? "N/A";
                
                //If ALL shifts -> Add all of them -> Else just add the one
                if(shiftID == -1) 
                    shifts = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);
                else 
                    shifts.Add(JsonConvert.DeserializeObject<Shift>(rawResponse));


                SpectreDisplay.DisplayTable<Shift>(Shift.headers, shifts);
            }

        }
        catch (Exception e) {
            System.Console.WriteLine("" + e.Message);
        }
    }

    internal void RemoveShift()
    {
        SpectreDisplay.DisplayHeader("Remove Shift");
        this.DisplayShifts();

        int shiftID = Utilities.GetShiftID();
        if(shiftID != -1)
        {
            //We want to delete a shift

            try
            {
                RestRequest request = new($"{shiftID}");
                var response = Utilities.client.ExecuteDelete(request);
                
                if(response.IsSuccessStatusCode)
                    System.Console.WriteLine($"Successfully Deleted Shift {shiftID}!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error: {e.Message}");
            }
        }
        Utilities.PressToContinue();
    }

    internal void AddShift()
    {
        SpectreDisplay.DisplayHeader("Add Shift");
        Shift shift = Utilities.CreateShift();

        try
        {
            RestRequest request = new();
            request.AddBody(shift);
            var response = Utilities.client.ExecutePost(request);

            if(response.IsSuccessStatusCode)
                System.Console.WriteLine("Successfully added Shfit!");

        }catch(Exception e)
        {
            System.Console.WriteLine($"Error: {e.Message}");
        }

        Utilities.PressToContinue();
    }

    internal void UpdateShift()
    {
        SpectreDisplay.DisplayHeader("Update Shift");
        this.DisplayShifts();
        int shiftID = Utilities.GetShiftID();
        
        if (shiftID != -1)
        {
            // Create the Updated Shift
            Shift updatedShift = Utilities.CreateShift();
            updatedShift.Id = shiftID;

            try
            {   
                RestRequest request = new($"{shiftID}");
                request.AddBody(updatedShift);

                var response = Utilities.client.ExecutePut(request);
                if(response.IsSuccessStatusCode)
                    System.Console.WriteLine($"Sucessfully Updated Shift ID: {shiftID}");
                else
                    System.Console.WriteLine($"Failed to Update the Shift, Status: {response.StatusCode}");
            
            }catch(Exception e)
            {
                System.Console.WriteLine($"Error: {e.Message}");
            }

            Utilities.PressToContinue();
        }
    }

}