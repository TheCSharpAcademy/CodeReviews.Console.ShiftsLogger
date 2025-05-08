using FrontEnd.Services;

using ShiftsLogger.Ryanw84.Models;
using ShiftsLogger.Ryanw84.Services;

namespace FrontEnd.Controllers;

internal class WorkerController()
	{
	

	internal async Task<List<Worker>> GetAllWorkers( )
		{
	
		var httpClient = new HttpClient();
		var uiShiftService = new UiShiftService(httpClient);
		var uiWorkerService = new UiWorkerService(httpClient);
		var workers = await uiWorkerService.GetAllWorkersAsync();
		return workers;
		}
	}
