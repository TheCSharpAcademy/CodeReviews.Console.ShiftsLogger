namespace ShiftsLoggerConsoleUI
{
	internal static class DataAccess
	{
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
						// display via table later
						Console.ReadLine();
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
		}
	}
}
