

using Bogus;

namespace ShiftsLoggerUI;

internal class ShiftFaker
{
	private readonly Faker<ShiftModel> _faker;

	public ShiftFaker()
	{
		_faker = new Faker<ShiftModel>()
			.RuleFor(s => s.Id, f => f.IndexFaker + 1) // Incremental ID
			.RuleFor(s => s.EmployeeId, f => f.Random.Int(1, 100)) // Random Employee ID
			.RuleFor(s => s.StartTime, f => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now)) // Random start within last 30 days
			.RuleFor(s => s.EndTime, (f, s) => s.StartTime.AddHours(f.Random.Double(4, 12))) // Random shift duration (4-12 hours)
			.RuleFor(s => s.ShiftType, f => f.PickRandom(new[] { "Morning", "Afternoon", "Night" })) // Random shift type
			.RuleFor(s => s.Notes, f => f.Lorem.Sentence()); // Random notes
	}
	public List<ShiftModel> GenerateShifts(int count)
	{
		return _faker.Generate(count);
	}
}
