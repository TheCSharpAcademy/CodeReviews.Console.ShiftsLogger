using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Repository;

namespace ShiftsLogger.IntegrationTests.Repositories;
public class UsersRepositoryTests
{
	[Fact]
	public async Task GetAll_ReturnsListOfUsers_WhenDataExists()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);

		var result = await userRepository.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Count.Should().Be(10);
	}

	[Fact]
	public async Task GetAll_ReturnsUnsuccessfulResponse_WhenDataDoesNotExist()
	{
		var dbContext = GetDatabaseContextWithoutUsers();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);

		var result = await userRepository.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("No users found!");
		result.Data.Should().BeNull();
	}

	[Fact]
	public async Task GetAll_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);

		dbContext.Dispose();

		var result = await userRepository.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task GetById_ReturnsUser_WhenUserExists()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = dbContext.Users.First().Id;

		var result = await userRepository.GetByIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Id.Should().Be(userId);
	}

	[Fact]
	public async Task GetById_ReturnsUnsuccessfulResponse_WhenUserDoesNotExist()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = Guid.NewGuid();

		var result = await userRepository.GetByIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be($"User with Id {userId} doesn't exist");
	}

	[Fact]
	public async Task GetById_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = Guid.NewGuid();

		dbContext.Dispose();

		var result = await userRepository.GetByIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task Create_ReturnsUser_WhenUserIsCreated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var user = new User
		{
			Id = Guid.NewGuid(),
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com",
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now
		};

		var result = await userRepository.CreateAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().BeEquivalentTo(user);
	}

	[Fact]
	public async Task Create_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var user = new User();

		dbContext.Dispose();

		var result = await userRepository.CreateAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task Delete_ReturnsSuccessfulResponse_WhenUserIsDeleted()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var trackedUser = dbContext.Users.First();
		dbContext.Entry(trackedUser).State = EntityState.Detached;
		var user = mapper.Map<User>(trackedUser);

		var result = await userRepository.DeleteAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Succesfully deleted!");
	}

	[Fact]
	public async Task Delete_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var user = new User();

		dbContext.Dispose();

		var result = await userRepository.DeleteAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task Update_ReturnsSuccessfulResponse_WhenUserIsUpdated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var trackedUser = dbContext.Users.First();
		dbContext.Entry(trackedUser).State = EntityState.Detached;
		trackedUser.FirstName = "Mike";
		var user = mapper.Map<User>(trackedUser);
		var result = await userRepository.UpdateAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Successfully updated!");
	}

	[Fact]
	public async Task Update_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var user = new User();

		dbContext.Dispose();

		var result = await userRepository.UpdateAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task CreateMany_ReturnsListOfUsers_WhenUsersAreCreated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var users = new List<User> {
		new User {
			Id = Guid.NewGuid(),
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com",
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now
		},
		new User{
			Id = Guid.NewGuid(),
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com",
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now
		}};

		var result = await userRepository.CreateManyAsync(users);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().BeEquivalentTo(users);
	}

	[Fact]
	public async Task CreateMany_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var users = new List<User>();

		dbContext.Dispose();

		var result = await userRepository.CreateManyAsync(users);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task UpdateShifts_ReturnsSuccessfulResponse_WhenShiftsAreUpdated()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = dbContext.Users.First().Id;
		var shiftsList = new List<Shift> {
			new Shift
			{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddHours(10),
			}};

		var result = await userRepository.UpdateShiftsAsync(userId, shiftsList);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be("Successfuly updated!");
	}

	[Fact]
	public async Task UpdateShifts_ReturnsUnsuccessfulResponse_WhenUserDoesNotExist()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = Guid.NewGuid();

		var result = await userRepository.UpdateShiftsAsync(userId, new List<Shift>());

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be($"User with Id {userId} doesn't exist");
	}

	[Fact]
	public async Task UpdateShifts_ReturnsUnsuccessfulResponse_WhenNoChangesToUpdate()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = dbContext.Users.First().Id;
		var shiftEntities = dbContext.Users.Where(u => u.Id == userId).SelectMany(u => u.Shifts).ToList();
		var shifts = mapper.Map<List<Shift>>(shiftEntities);

		var result = await userRepository.UpdateShiftsAsync(userId, shifts);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("No changes to update!");
	}

	[Fact]
	public async Task UpdateShifts_ReturnsUnsuccessfulResponse_WhenShiftsOverlap()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = dbContext.Users.First().Id;
		var shiftEntities = dbContext.Users.Where(u => u.Id == userId).SelectMany(u => u.Shifts).ToList();
		var shifts = mapper.Map<List<Shift>>(shiftEntities);
		var shiftsList = new List<Shift> {
			new Shift
			{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddHours(10),
			}};
		shiftsList.AddRange(shifts);

		var result = await userRepository.UpdateShiftsAsync(userId, shiftsList);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("Overlap with an already assigned shift!");
	}

	[Fact]
	public async Task UpdateShifts_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = Guid.NewGuid();
		var shifts = new List<Shift>();

		dbContext.Dispose();

		var result = await userRepository.UpdateShiftsAsync(userId, shifts);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsListOfShifts_WhenDataExists()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = dbContext.Users.First().Id;

		var result = await userRepository.GetShiftsByUserIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Count.Should().BeGreaterThan(0);
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsUnsuccessfulResponse_WhenDataDoesNotExist()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = Guid.NewGuid();

		var result = await userRepository.GetShiftsByUserIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("No shifts found!");
		result?.Data?.Count.Should().Be(0);
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsFailedResponse_WhenExceptionOccurs()
	{
		var dbContext = await GetDatabaseContext();
		var mapper = SetUpAutomapper();
		var userRepository = new UserRepository(dbContext, mapper);
		var userId = Guid.NewGuid();

		dbContext.Dispose();

		var result = await userRepository.GetShiftsByUserIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Contain("Error in UserRepository");
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

	private static ShiftsContext GetDatabaseContextWithoutUsers()
	{
		var options = new DbContextOptionsBuilder<ShiftsContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		var dbContext = new ShiftsContext(options);
		dbContext.Database.EnsureCreated();
		return dbContext;
	}
}
