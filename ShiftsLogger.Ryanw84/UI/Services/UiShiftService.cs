using FrontEnd.Controllers;

using Microsoft.EntityFrameworkCore;

using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Models;

using Spectre.Console;

namespace FrontEnd.Services;

public class UIShiftService
	{
	private readonly ShiftsDbContext _dbContext;

	public UIShiftService(ShiftsDbContext dbContext)
		{
		_dbContext = dbContext;
		}

	public List<Shift> GetAllShifts( )
		{
		return _dbContext.Shift
			.Include(s => s.Worker)
			.Include(s => s.Location)
			.ToList();
		}
	internal List<Shift> GetShiftByID( )
		{
		var shiftId = AnsiConsole.Ask<int>("Enter the [green]Shift ID[/]: ");
		return _dbContext.Shift
			.Include(s => s.Worker)
			.Include(s => s.Location)
			.Where(s => s.Id == shiftId)
			.ToList();
		}
	internal List<Shift> GetAllWorkers( )
		{
		return _dbContext.Shift
			.Include(s => s.Worker)
			.Include(s => s.Location)
			.ToList();
		}
	internal Worker GetWorkerOptionInput( )
		{
		var workers = WorkerController.GetAllWorkers();
		var workersArray = workers.Select(w => w.Name).ToArray();
		var option = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Select a [green]Worker[/]:")
				.AddChoices(workersArray));
				var id = workers.SingleOrDefault(w => w.Name == option).WorkerId;
		var worker = WorkerController.GetWorkerById(id);
		return worker;
		}
	}
