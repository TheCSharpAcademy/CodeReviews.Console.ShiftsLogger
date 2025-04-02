using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interfaces;
using ShiftsLoggerAPI.Models;


namespace ShiftsLoggerAPI.Repository;

public class SeedRepository : ISeedRepository
{
    private readonly ShitftsLoggerDbContext _context;
    private readonly Random _random;
    private const int DateRangeStart = -30;
    private const int DateRangeEnd = 1;

    public SeedRepository(ShitftsLoggerDbContext context)
    {
        _context = context;
        _random = new Random();
    }

    public async Task SeedEmployeesAsync()
    {
        var empNames = new List<string> { "Ahmad", "Basil", "Chris", "Osama", "Hera" }; // ranmdom employee profiles
        var empPhones = new List<string> { "0787964386", "0788038785", "078929385", "07869347814", "0789781294" };

        for (int i = 0; i < empNames.Count; i++)
        {
            var empName = empNames[i];
            var randPhone = empPhones[_random.Next(0, empPhones.Count)];

            await _context.Employees.AddAsync(
            new Employee
            {
                EmpName = empName,
                EmpPhone = randPhone
            }
            );
        }
        _context.SaveChanges();
    }

    public async Task SeedShiftsAsync(int randRowNumber)
    {
        var employees = await _context.Employees.AsNoTracking().ToListAsync();

        for (int i = 0; i < randRowNumber; i++)
        {
            var empIndex = _random.Next(0, employees.Count);
            var id = employees[empIndex].EmpId;
            var randShiftDate = RandomDate();
            var randStartTime = RandomTime(randShiftDate);
            var randEndTime = RandomTime(randShiftDate);

            while (randEndTime < randStartTime | randEndTime.Subtract(randStartTime).Hours == 0 | randEndTime.Subtract(randStartTime).Hours > 12)
            {
                randStartTime = RandomTime(randShiftDate);
                randEndTime = RandomTime(randShiftDate);
            }

            var randShift = new Shift
            {
                EmpId = id,
                StartDateTime = randStartTime,
                EndDateTime = randEndTime
            };

            await _context.AddAsync(randShift);
            _context.SaveChanges();
        }
    }

    public Task SeedShiftsAsync()
    {
        throw new NotImplementedException();
    }

    private DateTime RandomTime(DateTime shiftDate)
    {
        TimeSpan start = TimeSpan.FromHours(6);
        TimeSpan end = TimeSpan.FromHours(30);

        int maxMinutes = (int)((end - start).TotalMinutes);
        int minutes = _random.Next(maxMinutes);

        TimeSpan time = start.Add(TimeSpan.FromMinutes(minutes)); // 24 hr format
        DateTime timeOfDay = shiftDate.Add(time); // converts to 12hr AM/PM format due to database specifications

        return timeOfDay;
    }

    private DateTime RandomDate()
    {
        var randOffset = _random.Next(DateRangeStart, DateRangeEnd);
        return DateTime.Today.AddDays(randOffset); // random dates within the last month.
    }


}
