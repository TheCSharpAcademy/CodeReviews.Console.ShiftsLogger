using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Repository;

namespace ShiftsLogger.IntegrationTests.Repositories;
public class SeedingRepositoryTests
{
	[Fact]
	public async Task RecordsExists_ReturnsFalse_WhenDatabaseHasNoData()
	{
		var dbContext = GetDatabaseContextWithoutData();
		var mapper = SetUpAutomapper();
		var seedingRepository = new SeedingRepository(dbContext, mapper);

		var result = await seedingRepository.RecordsExistAsync();

		result.Should().BeFalse();
	}

	[Fact]
	public async Task RecordsExists_ReturnsTrue_WhenDatabaseHasData()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var seedingRepository = new SeedingRepository(dbContext, mapper);

		var result = await seedingRepository.RecordsExistAsync();

		result.Should().BeTrue();
	}

	[Fact]
	public async Task AddUsersAndShifts_ReturnsSuccessfulResponse_WhenDatabaseIsSeeded()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var seedingRepository = new SeedingRepository(dbContext, mapper);
		var users = new List<User>();
		var shifts = new List<Shift>();

		var result = await seedingRepository.AddUsersAndShiftsAsync(users, shifts);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Database seeded successfully!");
	}

	[Fact]
	public async Task AddUsersAndShifts_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var seedingRepository = new SeedingRepository(dbContext, mapper);
		var users = new List<User>();
		var shifts = new List<Shift>();

		dbContext.Dispose();

		var result = await seedingRepository.AddUsersAndShiftsAsync(users, shifts);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in SeedingRepository");
	}

	private static IMapper SetUpAutomapper()
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new RepositoryProfile());
		});
		var mapper = config.CreateMapper();
		return mapper;
	}

	private static async Task<ShiftsContext> GetDatabaseContext()
	{
		var options = new DbContextOptionsBuilder<ShiftsContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		var dbContext = new ShiftsContext(options);
		dbContext.Database.EnsureCreated();
		if (!await dbContext.Users.AnyAsync())
		{
			for (int i = 0; i < 10; i++)
			{
				var user = new UserEntity
				{
					Id = Guid.NewGuid(),
					FirstName = "John",
					LastName = "Doe",
					Email = "jdoe@gmail.com",
					IsActive = true,
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now
				};
				user.Shifts.Add(new ShiftEntity
				{
					Id = Guid.NewGuid(),
					StartTime = DateTime.Now,
					EndTime = DateTime.Now.AddHours(8),
					IsActive = true,
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now
				});
				dbContext.Add(user);
			}
			await dbContext.SaveChangesAsync();
		}
		return dbContext;
	}

	private static ShiftsContext GetDatabaseContextWithoutData()
	{
		var options = new DbContextOptionsBuilder<ShiftsContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		var dbContext = new ShiftsContext(options);
		dbContext.Database.EnsureCreated();
		return dbContext;
	}
}
