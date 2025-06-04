using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Repository;

namespace ShiftsLogger.IntegrationTests.Repositories;
public class ShiftsRepositoryTests
{
	[Fact]
	public async Task GetAll_ReturnsListOfShifts_WhenDataExists()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);

		var result = await shiftRepository.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Count.Should().Be(10);
	}

	[Fact]
	public async Task GetAll_ReturnsUnsuccessfulResponse_WhenDataDoesNotExist()
	{
		var dbContext = GetDatabaseContextWithoutShifts();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);

		var result = await shiftRepository.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("No shifts found!");
		result.Data.Should().BeNull();
	}

	[Fact]
	public async Task GetAll_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);

		dbContext.Dispose();

		var result = await shiftRepository.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task GetById_ReturnsShift_WhenUserExists()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = dbContext.Shifts.First().Id;

		var result = await shiftRepository.GetByIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Id.Should().Be(shiftId);
	}

	[Fact]
	public async Task GetById_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = Guid.NewGuid();

		var result = await shiftRepository.GetByIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be($"Shift with Id {shiftId} doesn't exist");
	}

	[Fact]
	public async Task GetById_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = Guid.NewGuid();

		dbContext.Dispose();

		var result = await shiftRepository.GetByIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task Create_ReturnsShift_WhenShiftIsCreated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shift = new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddHours(8),
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now
		};

		var result = await shiftRepository.CreateAsync(shift);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().BeEquivalentTo(shift);
	}

	[Fact]
	public async Task Create_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shift = new Shift();

		dbContext.Dispose();

		var result = await shiftRepository.CreateAsync(shift);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task Delete_ReturnsSuccessfulResponse_WhenShiftIsDeleted()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var trackedShift = dbContext.Shifts.First();
		dbContext.Entry(trackedShift).State = EntityState.Detached;
		var shift = mapper.Map<Shift>(trackedShift);

		var result = await shiftRepository.DeleteAsync(shift);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Succesfully deleted!");
	}

	[Fact]
	public async Task Delete_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shift = new Shift();

		dbContext.Dispose();

		var result = await shiftRepository.DeleteAsync(shift);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task Update_ReturnsSuccessfulResponse_WhenShiftIsUpdated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var trackedShift = dbContext.Shifts.First();
		dbContext.Entry(trackedShift).State = EntityState.Detached;
		trackedShift.StartTime = DateTime.Now.AddHours(2);
		var shift = mapper.Map<Shift>(trackedShift);

		var result = await shiftRepository.UpdateAsync(shift);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Successfully updated!");
	}

	[Fact]
	public async Task Update_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shift = new Shift();

		dbContext.Dispose();

		var result = await shiftRepository.UpdateAsync(shift);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task CreateMany_ReturnsListOfShifts_WhenShiftsAreCreated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shifts = new List<Shift>
		{
			new Shift
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Now,
				EndTime = DateTime.Now.AddHours(8),
				IsActive = true,
				DateCreated = DateTime.Now,
				DateUpdated = DateTime.Now
			},
			new Shift
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Now.AddHours(2),
				EndTime = DateTime.Now.AddHours(10),
				IsActive = true,
				DateCreated = DateTime.Now,
				DateUpdated = DateTime.Now
			}
		};

		var result = await shiftRepository.CreateManyAsync(shifts);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().BeEquivalentTo(shifts);
	}

	[Fact]
	public async Task CreateMany_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shifts = new List<Shift>();

		dbContext.Dispose();

		var result = await shiftRepository.CreateManyAsync(shifts);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task UpdateUsers_ReturnsSuccessfulResponse_WhenUsersAreUpdated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = dbContext.Shifts.First().Id;
		var users = new List<User>
		{
			new User
			{
				Id = Guid.NewGuid(),
				FirstName = "John",
				LastName = "Doe",
				Email = "jdoe@gmail.com",
				IsActive = true,
				DateCreated = DateTime.Now,
				DateUpdated = DateTime.Now,
			}
		};

		var result = await shiftRepository.UpdateUsersAsync(shiftId, users);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Successfuly updated!");
	}

	[Fact]
	public async Task UpdateUsers_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = Guid.NewGuid();

		var result = await shiftRepository.UpdateUsersAsync(shiftId, new List<User>());

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be($"Shift with Id {shiftId} doesn't exist");
	}

	[Fact]
	public async Task UpdateUsers_ReturnsUnsuccessfulResponse_WhenNoChangesToUpdate()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = dbContext.Shifts.First().Id;
		var userEntities = dbContext.Shifts.Where(s => s.Id == shiftId).SelectMany(s => s.Users).ToList();
		var users = mapper.Map<List<User>>(userEntities);

		var result = await shiftRepository.UpdateUsersAsync(shiftId, users);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("No changes to update!");
	}

	[Fact]
	public async Task UpdateUsers_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = Guid.NewGuid();
		var users = new List<User>();

		dbContext.Dispose();

		var result = await shiftRepository.UpdateUsersAsync(shiftId, users);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsListOfUsers_WhenDataExists()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = dbContext.Shifts.First().Id;

		var result = await shiftRepository.GetUsersByShiftIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Count.Should().BeGreaterThan(0);
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsUnsuccessfulResponse_WhenDataDoesNotExist()
	{
		var dbContext = GetDatabaseContextWithoutShifts();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = Guid.NewGuid();

		var result = await shiftRepository.GetUsersByShiftIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("No users found!");
		result?.Data?.Count.Should().Be(0);
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var shiftRepository = new ShiftRepository(dbContext, mapper);
		var shiftId = Guid.NewGuid();

		dbContext.Dispose();

		var result = await shiftRepository.GetUsersByShiftIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in ShiftRepository");
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
		if (!await dbContext.Shifts.AnyAsync())
		{
			for (int i = 0; i < 10; i++)
			{
				var shift = new ShiftEntity
				{
					Id = Guid.NewGuid(),
					StartTime = DateTime.Now,
					EndTime = DateTime.Now.AddHours(8),
					IsActive = true,
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now
				};
				shift.Users.Add(new UserEntity
				{
					Id = Guid.NewGuid(),
					FirstName = "John",
					LastName = "Doe",
					Email = "jdoe@gmail.com",
					IsActive = true,
					DateCreated = DateTime.Now,
					DateUpdated = DateTime.Now
				});
				dbContext.Add(shift);
			}
			await dbContext.SaveChangesAsync();
		}
		return dbContext;
	}

	private static ShiftsContext GetDatabaseContextWithoutShifts()
	{
		var options = new DbContextOptionsBuilder<ShiftsContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		var dbContext = new ShiftsContext(options);
		dbContext.Database.EnsureCreated();
		return dbContext;
	}
}
