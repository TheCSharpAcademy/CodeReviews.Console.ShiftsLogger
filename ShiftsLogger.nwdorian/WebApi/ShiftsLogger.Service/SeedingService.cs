using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.Service;
public class SeedingService : ISeedingService
{
	public SeedingService(ISeedingRepository seedingRepository)
	{
		_seedingRepository = seedingRepository;
	}
	public async Task<ApiResponse<string>> SeedDataAsync()
	{
		var response = new ApiResponse<string>();

		if (await _seedingRepository.RecordsExistAsync())
		{
			response.Success = false;
			response.Message = "Records already exist in the database!";
		}
		else
		{
			response = await _seedingRepository.AddUsersAndShiftsAsync(_users, _shifts);
		}
		return response;
	}

	private readonly List<User> _users = new List<User>
		{
				new User
				{
						Id = Guid.Parse("0bea3051-c125-40ed-a62f-68c2d7df05bd"),
						FirstName = "John",
						LastName = "Doe",
						Email = "jdoe@gmail.com",
						IsActive = true,
						DateCreated = DateTime.Now,
						DateUpdated = DateTime.Now
				},
				new User
				{
						Id = Guid.Parse("fd3788d8-b1b3-47fd-883a-8c2ed561b29c"),
						FirstName = "Ada",
						LastName = "Lovelace",
						Email = "alovelace@gmail.com",
						IsActive = true,
						DateCreated = DateTime.Now,
						DateUpdated = DateTime.Now
				},
				new User
				{
						Id = Guid.Parse("8ac24687-8ee2-4853-9867-3eb3b7498ac5"),
						FirstName = "Bobby",
						LastName = "Miller",
						Email = "bmiller@gmail.com",
						IsActive = true,
						DateCreated = DateTime.Now,
						DateUpdated = DateTime.Now
				}
		};

	private readonly List<Shift> _shifts = new List<Shift>
		{
				new Shift
				{
						Id = Guid.Parse("c31cd365-489f-4326-a17e-27b6f2eeccb9"),
						StartTime = DateTime.Today.AddDays(1).AddHours(9),
						EndTime = DateTime.Today.AddDays(1).AddHours(17),
						IsActive = true,
						DateCreated = DateTime.Now,
						DateUpdated = DateTime.Now
				},
				new Shift
				{
						Id = Guid.Parse("04ee48f0-dde0-41f3-abba-8dacfde2d2b1"),
						StartTime = DateTime.Today.AddDays(2).AddHours(8),
						EndTime = DateTime.Today.AddDays(2).AddHours(16),
						IsActive = true,
						DateCreated = DateTime.Now,
						DateUpdated = DateTime.Now
				},
				new Shift
				{
						Id = Guid.Parse("16f09672-9e48-4b59-bb0d-f3460681bfe4"),
						StartTime = DateTime.Today.AddDays(3).AddHours(7),
						EndTime = DateTime.Today.AddDays(3).AddHours(15),
						IsActive = true,
						DateCreated = DateTime.Now,
						DateUpdated = DateTime.Now
				}
		};
	private readonly ISeedingRepository _seedingRepository;
}
