using ShiftsLogger.WebApi.Models;

namespace ShiftsLogger.WebApi.Data;

internal static class DbInitializer
{
    internal static async Task Seed(WorkerShiftContext context)
    {
        if (!context.Workers.Any() && !context.Shifts.Any())
        {
            List<Worker> sampleWorkers = GenerateWorkers();
            await context.Workers.AddRangeAsync(sampleWorkers);
            await context.SaveChangesAsync();

            List<Shift> sampleShifts = GenerateShifts(sampleWorkers);
            await context.Shifts.AddRangeAsync(sampleShifts);
            await context.SaveChangesAsync();
        }
    }

    private static List<Worker> GenerateWorkers()
    {
        List<Worker> sampleWorkers = new();
        Random random = new Random();
        string[] sampleName = new string[] { "Allison", "Barry", "Charlie", "Dale", "Elizabeth", "Fatima", "Gio", "Harry", "Ilana", "Jack" };

        for (int i = 1; i <= 10; i++)
        {
            Worker worker = new Worker()
            {
                Name = sampleName[i-1],
            };
            sampleWorkers.Add(worker);
        }
        return sampleWorkers;
    }

    private static List<Shift> GenerateShifts(List<Worker> workers)
    {
        List<Shift> sampleShifts = new();
        Random random = new();

        for (int i = 1; i < 151; i++)
        {
            DateTime start = new DateTime(2025, 1, 1);
            int rangeDays = (DateTime.Today - start).Days;
            DateTime shiftDate = start.AddDays(random.Next(rangeDays));

            DateTime shiftStartTime = shiftDate.AddHours(random.Next(24)).AddMinutes(random.Next(60));
            DateTime shiftEndTime = shiftStartTime.AddHours(random.Next(1, 17)).AddMinutes(random.Next(60));

            int randomWorkerId = random.Next(1, 11);
            Worker? shiftWorker = workers.Where(w => w.Id == randomWorkerId).SingleOrDefault();

            if (shiftWorker != null)
            {
                Shift shift = new Shift()
                {
                    WorkerId = randomWorkerId,
                    StartTime = shiftStartTime,
                    EndTime = shiftEndTime,
                    Worker = shiftWorker
                };
                sampleShifts.Add(shift); 
            }
        }
        return sampleShifts;
    }
}
