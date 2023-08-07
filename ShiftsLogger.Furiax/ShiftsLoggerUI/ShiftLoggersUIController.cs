using ShiftsLoggerUI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ShiftsLoggerUI
{
	internal class ShiftLoggersUIController
	{
		internal static async void AddShift(Shift shift)
		{
			string serializeShift = JsonSerializer.Serialize(shift);
			using (HttpClient client = new HttpClient())
			{
				HttpContent shiftContent = new StringContent(serializeShift, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await client.PostAsync("https://localhost:7054/api/ShiftsLogger/", shiftContent).ConfigureAwait(false);
			}
		}

		internal static async Task DeleteShift(int shiftId)
		{
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7054/api/ShiftsLogger/{shiftId}").ConfigureAwait(false);
			}
		}

		internal static async Task<Shift> GetEmployeeShiftById(int id)
		{
			using HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/ShiftsLogger"));
			await using Stream stream =
				await client.GetStreamAsync($"https://localhost:7054/api/ShiftsLogger/{id}");
			Shift shift = await JsonSerializer.DeserializeAsync<Shift>(stream);
			return shift;
		}

		internal static async Task<List<Shift>> GetShifts()
		{
			List<Shift> shifts = new();

			using HttpClient client = new();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/ShiftsLogger"));
			await using Stream stream =
					await client.GetStreamAsync("https://localhost:7054/api/ShiftsLogger/");
			shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream);
			return shifts;
		}

		internal static async Task UpdateShift(Shift shift)
		{
			string serializeShift = JsonSerializer.Serialize(shift);
			using (HttpClient client = new HttpClient())
			{
				HttpContent shiftContent = new StringContent(serializeShift, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await client.PutAsync($"https://localhost:7054/api/ShiftsLogger/{shift.Id}", shiftContent);
			}
		}
	}
}
