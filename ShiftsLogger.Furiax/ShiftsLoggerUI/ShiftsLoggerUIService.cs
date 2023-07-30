using RestSharp;

namespace ShiftsLoggerUI
{
	internal class ShiftsLoggerUIService
	{
		internal static async void GetShifts()
		{
			var client = new RestClient("https://localhost:7054/api/ShiftsLogger/");
			var request = new RestRequest("");
			var response = await client.GetAsync(request);

			if (response.IsSuccessStatusCode)
			{
				//returns the raw json file, next up I need to put it into a model and display it
				string rawResponse = response.Content;
                Console.WriteLine(rawResponse);
            }
			else
			{
				Console.WriteLine("Request failed");
			}
		}
	}
}
