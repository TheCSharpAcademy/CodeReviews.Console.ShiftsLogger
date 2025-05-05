using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Mapping;
using ShiftsLogger.Ryanw84.Services;
using Spectre.Console;

namespace ShiftsLogger.Ryanw84.UI
{
    public class Program
    {
        private ShiftsDbContext dbContext;
        private IMapper mapper;

        public Program()
        {
            var options = new DbContextOptionsBuilder<ShiftsDbContext>()
                .UseSqlServer(
                    @"Server=(localdb)\MSSQLlocaldb; Database=ShiftsLogger; Integrated Security=True; TrustServerCertificate=True;"
                )
                .Options;
            dbContext = new ShiftsDbContext(options);
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = mapperConfig.CreateMapper();
        }

        public void MainMenu()
        {
            bool isMenuRunning = true;

            var shiftService = new ShiftService(dbContext, mapper);

            while (isMenuRunning)
            {
                var console = AnsiConsole.Console;
                var prompt = new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices("Add Shift", "View Shifts", "Exit");

                var choice = console.Prompt(prompt);

                switch (choice)
                {
                    case "Add Shift":
                        AddShift(shiftService);
                        break;
                    case "View Shifts":
                        ViewShifts(shiftService);
                        break;
                    case "Exit":
                        isMenuRunning = false;
                        break;
                }
            }
        }

        private void AddShift(ShiftService shiftService)
        {
            var workerName = AnsiConsole.Ask<string>("Enter worker name:");
            var locationName = AnsiConsole.Ask<string>("Enter location name:");
            var date = AnsiConsole.Ask<DateTime>("Enter shift date (yyyy-MM-dd):");
            var startTime = AnsiConsole.Ask<TimeSpan>("Enter start time (HH:mm):");
            var endTime = AnsiConsole.Ask<TimeSpan>("Enter end time (HH:mm):");

            var shiftRequestDto = new ShiftApiRequestDto
            {
                WorkerName = workerName,
                ShiftStartTime = date.Add(startTime),
                ShiftEndTime = date.Add(endTime),
                ShiftDuration = endTime - startTime,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var result = shiftService.CreateShift(shiftRequestDto).Result;

            if (result.RequestFailed)
            {
                AnsiConsole.MarkupLine($"[red]Failed to add shift: {result.ErrorMessage}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
            }
        }

        private void ViewShifts(ShiftService shiftService)
        {
            var shiftsResponse = shiftService.GetAllShifts(new ShiftOptions()).Result;

            if (
                shiftsResponse.RequestFailed
                || shiftsResponse.Data == null
                || !shiftsResponse.Data.Any()
            )
            {
                AnsiConsole.MarkupLine("[yellow]No shifts found.[/]");
                return;
            }

            var table = new Table()
                .AddColumn("Date")
                .AddColumn("Start Time")
                .AddColumn("End Time")
                .AddColumn("Worker")
                .AddColumn("Location");

            foreach (var shift in shiftsResponse.Data)
            {
                table.AddRow(
                    shift.Date.ToString("yyyy-MM-dd"),
                    shift.StartTime.ToString("HH:mm"),
                    shift.EndTime.ToString("HH:mm"),
                    shift.Worker?.Name ?? "N/A",
                    shift.Location?.Name ?? "N/A"
                );
            }

            AnsiConsole.Write(table);
        }
    }
}
