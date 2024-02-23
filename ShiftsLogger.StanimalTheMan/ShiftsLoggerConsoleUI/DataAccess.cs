using ShiftsLoggerWebAPI.Models;
using System.Net.Http.Json;

namespace ShiftsLoggerConsoleUI
{
	internal static class DataAccess
	{
		internal static async Task CreateShiftAsync()
		{
			// maybe separate out logic to get all user input later, but this is part of creating a shift so it's here for now
			Console.WriteLine("Enter type of shift (e.g. night, mid, morning)");
			string shiftType = Console.ReadLine();

			Console.WriteLine("Enter Start Time of shift: ");
			var startTimeInfo = Utility.GetDateTimeInput();

			Console.WriteLine("Enter End Time of shift: ");
			var endTimeInfo = Utility.GetDateTimeInput();
			while (endTimeInfo.dateTime < startTimeInfo.dateTime)
			{
				Console.WriteLine("End Time");
				endTimeInfo = Utility.GetDateTimeInput();
			}

			Shift shift = null;
			TimeSpan duration = Utility.CalculateDuration(endTimeInfo.dateTime, startTimeInfo.dateTime);
			if (shiftType != "")
			{
				shift = new Shift()
				{
					Type = shiftType,
					StartTime = startTimeInfo.dateTime,
					EndTime = endTimeInfo.dateTime,
					Duration = duration
				};
			}
			else
			{
				shift = new Shift()
				{
					StartTime = startTimeInfo.dateTime,
					EndTime = endTimeInfo.dateTime,
					Duration = duration
				};
			}


			using (var httpClient = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7204/api/shifts/", shift);

					if (response.IsSuccessStatusCode)
					{
						var todo = await response.Content.ReadFromJsonAsync<Shift>();
						Console.WriteLine($"{shift}\n");
					}
					else
					{
						Console.WriteLine($"Failed to post data. Status code: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Server / API may be down or some other unexpected issue is going on: {ex.Message}");
					Console.ReadLine();
				}
			}
			Console.WriteLine("Press any key to return to main menu");
			Console.ReadLine();
		}

		internal static async Task DeleteShiftAsync(int id)
		{
			using (var httpClient = new HttpClient()){
				try
				{
					HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7204/api/shifts/{id}");
					if (response.IsSuccessStatusCode)
					{
						Console.WriteLine("Successfully deleted shift");
					}
					else
					{
						Console.WriteLine("Failed to delete shift");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Server / API may be down or some other unexpected issue is going on: {ex.Message}");
				}
			}

			Console.WriteLine("Press any key to return to main menu");
			Console.ReadLine();
		}

		internal static async Task GetAllShiftsAsync()
		{
			using (var httpClient = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7204/api/shifts/");

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();
						Console.WriteLine("Response from API:");
						Console.WriteLine(responseBody);
					}
					else
					{
						Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Server / API may be down or some other unexpected issue is going on: {ex.Message}");
				}
			}
			Console.WriteLine("Press any key to return to main menu");
			Console.ReadLine();
		}

		internal static async Task GetShiftByIdAsync(int id)
		{
			using (var httpClient = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7204/api/shifts/{id}");

					if (response.IsSuccessStatusCode)
					{
						string responseBody = await response.Content.ReadAsStringAsync();
						Console.WriteLine("Response from API:");
						Console.WriteLine(responseBody);
					}
					else
					{
						Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Server / API may be down or some other unexpected issue is going on: {ex.Message}");
				}
			}
			Console.WriteLine("Press any key to return to main menu");
			Console.ReadLine();
		}

		internal static async Task UpdateShiftAsync(int id)
		{
			// do I fetch the Task with id?
			// maybe separate out logic to get all user input later, but this is part of creating a shift so it's here for now
			Console.WriteLine("Enter type of shift (e.g. night, mid, morning)");
			string shiftType = Console.ReadLine();

			Console.WriteLine("Enter Start Time of shift: ");
			var startTimeInfo = Utility.GetDateTimeInput();

			Console.WriteLine("Enter End Time of shift: ");
			var endTimeInfo = Utility.GetDateTimeInput();
			while (endTimeInfo.dateTime < startTimeInfo.dateTime)
			{
				Console.WriteLine("End Time");
				endTimeInfo = Utility.GetDateTimeInput();
			}

			Shift shift = null;
			TimeSpan duration = Utility.CalculateDuration(endTimeInfo.dateTime, startTimeInfo.dateTime);
			if (shiftType != "")
			{
				shift = new Shift()
				{
					Id = id,
					Type = shiftType,
					StartTime = startTimeInfo.dateTime,
					EndTime = endTimeInfo.dateTime,
					Duration = duration
				};
			}
			else
			{
				shift = new Shift()
				{
					Id = id,
					StartTime = startTimeInfo.dateTime,
					EndTime = endTimeInfo.dateTime,
					Duration = duration
				};
			}


			using (var httpClient = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:7204/api/shifts/{id}", shift);

					if (response.IsSuccessStatusCode)
					{
						Console.WriteLine("Shift successfully updated");
					}
					else
					{
						Console.WriteLine($"Failed to update shift. Status code: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Server / API may be down or some other unexpected issue is going on: {ex.Message}");
					Console.ReadLine();
				}
			}
			Console.WriteLine("Press any key to return to main menu");
			Console.ReadLine();
		}
	}
}
