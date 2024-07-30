using System.Web;
using Newtonsoft.Json;
using RestSharp;
using ShiftsLogger.ConsoleApp.Models;
using ShiftsLogger.ConsoleApp.Views;

namespace ShiftsLogger.ConsoleApp.Services;

/// <summary>
/// Service to perform calls to the shift api and handle the responses.
/// </summary>
internal class ShiftApiService
{
    #region Constants

    private readonly static string Base = "http://localhost:5240/api/v1";
    private readonly static string CreateApiRoute = @$"{Base}/shifts";
    private readonly static string GetApiRoute = @$"{Base}/shifts";
    private readonly static string GetByIdApiRoute = @$"{Base}/shifts/{{shiftId}}";
    private readonly static string UpdateApiRoute = @$"{Base}/shifts/{{shiftId}}";
    private readonly static string DeleteApiRoute = @$"{Base}/shifts/{{shiftId}}";

    #endregion
    #region Methods

    internal static bool CreateShift(CreateShiftRequest shift)
    {
        var output = false;

        using var client = new RestClient();

        var request = new RestRequest(CreateApiRoute);
        request.AddBody(new
        {
            shift.StartTime,
            shift.EndTime,
        });

        try
        {
            var reponse = client.Execute(request, Method.Post);
            if (reponse.StatusCode is System.Net.HttpStatusCode.Created)
            {
                output = true;
            }
            else
            {
                throw new InvalidOperationException($"Invalid HTTP Status Code. Expected: {System.Net.HttpStatusCode.Created}. Actual: {reponse.StatusCode}.");
            }
        }
        catch (Exception exception)
        {
            MessagePage.Show(exception);
        }

        return output;
    }

    internal static bool DeleteShift(Guid shiftId)
    {
        var output = false;

        using var client = new RestClient();

        var request = new RestRequest(DeleteApiRoute.Replace("{shiftId}", HttpUtility.UrlEncode(shiftId.ToString())));

        try
        {
            var reponse = client.Execute(request, Method.Delete);
            if (reponse.StatusCode is System.Net.HttpStatusCode.NoContent)
            {
                output = true;
            }
            else
            {
                throw new InvalidOperationException($"Invalid HTTP Status Code. Expected: {System.Net.HttpStatusCode.NoContent}. Actual: {reponse.StatusCode}.");
            }
        }
        catch (Exception exception)
        {
            MessagePage.Show(exception);
        }

        return output;
    }

    internal static IReadOnlyList<ShiftDto> GetShifts()
    {
        IReadOnlyList<ShiftDto> output = [];

        using var client = new RestClient();

        var request = new RestRequest(GetApiRoute);

        try
        {
            var reponse = client.Execute(request, Method.Get);
            if (reponse.StatusCode is System.Net.HttpStatusCode.OK)
            {
                output = JsonConvert.DeserializeObject<IReadOnlyList<ShiftDto>>(reponse.Content!)!;
            }
            else
            {
                throw new InvalidOperationException($"Invalid HTTP Status Code. Expected: {System.Net.HttpStatusCode.OK}. Actual: {reponse.StatusCode}.");
            }
        }
        catch (Exception exception)
        {
            MessagePage.Show(exception);
        }

        return output;
    }

    internal static bool UpdateShift(UpdateShiftRequest shift)
    {
        var output = false;

        using var client = new RestClient();

        var request = new RestRequest(UpdateApiRoute.Replace("{shiftId}", HttpUtility.UrlEncode(shift.Id.ToString())));
        request.AddBody(new
        {
            shift.StartTime,
            shift.EndTime,
        });

        try
        {
            var reponse = client.Execute(request, Method.Put);
            if (reponse.StatusCode is System.Net.HttpStatusCode.OK)
            {
                output = true;
            }
            else
            {
                throw new InvalidOperationException($"Invalid HTTP Status Code. Expected: {System.Net.HttpStatusCode.OK}. Actual: {reponse.StatusCode}.");
            }
        }
        catch (Exception exception)
        {
            MessagePage.Show(exception);
        }

        return output;
    }

    #endregion
}
