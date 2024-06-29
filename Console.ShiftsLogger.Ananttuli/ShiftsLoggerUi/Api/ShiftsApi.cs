using System.Net.Http.Json;
using System.Text.Json;
using ShiftsLoggerUi.Api.Shifts;

namespace ShiftsLoggerUi.Api;

public class ShiftsApi(Client httpClient)
{
    readonly Client HttpClient = httpClient;

    public async Task<Response<List<ShiftDto>?>> GetShifts()
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["SHIFTS"]);

            await using Stream stream = await HttpClient.client.GetStreamAsync(endpoint);

            var shifts = await JsonSerializer
                .DeserializeAsync<List<ShiftDto>>(stream);

            return new Response<List<ShiftDto>?>(shifts != null, shifts ?? null);
        }
        catch
        {
            return new Response<List<ShiftDto>?>(false, null, "Could not fetch shifts");
        }
    }

    public async Task<Response<ShiftDto?>> GetShift(int shiftId)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["SHIFTS"]);

            await using Stream stream = await HttpClient.client.GetStreamAsync($"{endpoint}/{shiftId}");

            var shift = await JsonSerializer
                .DeserializeAsync<ShiftDto>(stream);

            return new Response<ShiftDto?>(shift != null, shift ?? null);
        }
        catch
        {
            return new Response<ShiftDto?>(false, null, "Could not fetch shift");
        }
    }

    public async Task<Response<ShiftDto?>> CreateShift(ShiftCreateDto shift)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["SHIFTS"]);

            var response = await HttpClient.client.PostAsJsonAsync(endpoint, shift);

            var (_, apiErrorMessage) = await ApiErrorResponse.ExtractErrorFromResponse(response);

            if (apiErrorMessage != null)
            {
                return new Response<ShiftDto?>(false, null, apiErrorMessage);
            }

            var createdShift = await response.Content.ReadFromJsonAsync<ShiftDto>();

            return new Response<ShiftDto?>(createdShift?.ShiftId != null, createdShift ?? null);
        }
        catch
        {
            return new Response<ShiftDto?>(false, null, "Unknown error");
        }
    }

    public async Task<Response<ShiftDto?>> UpdateShift(ShiftUpdateDto shift)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["SHIFTS"]);

            var response = await HttpClient.client.PutAsJsonAsync($"{endpoint}/{shift.ShiftId}", shift);

            var (_, apiErrorMessage) = await ApiErrorResponse.ExtractErrorFromResponse(response);

            if (apiErrorMessage != null)
            {
                return new Response<ShiftDto?>(false, null, apiErrorMessage);
            }

            var updatedShift = await response.Content.ReadFromJsonAsync<ShiftDto>();

            return new Response<ShiftDto?>(updatedShift?.ShiftId != null, updatedShift ?? null);
        }
        catch
        {
            return new Response<ShiftDto?>(false, null, "Unknown error");
        }
    }

    public async Task<Response<bool>> DeleteShift(int id)
    {
        try
        {
            var endpoint = ReqUtil.AssertNonNull(ConfigManager.ApiRoutes()["SHIFTS"]);

            var response = await HttpClient.client.DeleteAsync($"{endpoint}/{id}");

            var (_, apiErrorMessage) = await ApiErrorResponse.ExtractErrorFromResponse(response);

            if (apiErrorMessage != null)
            {
                return new Response<bool>(false, false, apiErrorMessage);
            }

            return new Response<bool>(true, true);
        }
        catch
        {
            return new Response<bool>(false, false, "Unknown error");
        }
    }
}

