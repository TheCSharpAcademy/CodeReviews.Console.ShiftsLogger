using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service;

namespace ShiftsLogger.UnitTests.Services;
public class UsersServiceTests
{
	private readonly UserService _userService;
	private readonly Mock<IUserRepository> _userRepositoryMock;
	public UsersServiceTests()
	{
		_userRepositoryMock = new Mock<IUserRepository>();
		_userService = new UserService(_userRepositoryMock.Object);
	}

	[Fact]
	public async Task GetAll_ReturnsListOfUsers_WhenRepositoryReturnsData()
	{
		var expectedResponse = new ApiResponse<List<User>>()
		{
			Data = GetUsers(),
			Success = true
		};
		_userRepositoryMock.Setup(repo => repo.GetAllAsync())
			.ReturnsAsync(expectedResponse);

		var result = await _userService.GetAllAsync();

		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result.Data.Should().HaveCount(expectedResponse.Data.Count);
		_userRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetAll_ReturnsUnsuccessfulResponse_WhenRepositoryReturnsNoData()
	{
		var expectedResponse = new ApiResponse<List<User>>()
		{
			Data = null,
			Success = false,
			Message = "No users found!"
		};
		_userRepositoryMock.Setup(repo => repo.GetAllAsync())
			.ReturnsAsync(expectedResponse);

		var result = await _userService.GetAllAsync();

		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(expectedResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetAll_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var exceptionMessage = "Database connection failed!";
		var expectedResponse = new ApiResponse<List<User>>()
		{
			Data = null,
			Success = false,
			Message = $"Error in UserRepository GetAllAsync: {exceptionMessage}"
		};
		_userRepositoryMock.Setup(repo => repo.GetAllAsync())
			.ReturnsAsync(expectedResponse);

		var result = await _userService.GetAllAsync();

		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(expectedResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsUser_WhenUserExists()
	{
		var user = GetUser();
		var expectedResponse = new ApiResponse<User>()
		{
			Data = user,
			Success = true
		};
		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id))
			.ReturnsAsync(expectedResponse);

		var result = await _userService.GetByIdAsync(user.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result.Data.Should().BeEquivalentTo(user);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsUnsuccessfulResponse_WhenUserDoesNotExist()
	{
		var userId = Guid.NewGuid();
		var expectedResponse = new ApiResponse<User>()
		{
			Success = false,
			Message = $"User with Id {userId} doesn't exist"
		};
		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
			.ReturnsAsync(expectedResponse);

		var result = await _userService.GetByIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(expectedResponse.Message);
	}

	[Fact]
	public async Task GetById_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var exceptionMessage = "Database connection failed!";
		var expectedResponse = new ApiResponse<User>()
		{
			Success = false,
			Message = $"Error in UserRepository GetByIdAsync: {exceptionMessage}"
		};
		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(expectedResponse);

		var result = await _userService.GetByIdAsync(Guid.NewGuid());

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(expectedResponse.Message);
	}

	[Fact]
	public async Task Create_ReturnsSucessfulResponse_WhenUserIsCreated()
	{
		var inputUser = new User()
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com"
		};

		var expectedUser = new User()
		{
			Id = Guid.NewGuid(),
			FirstName = inputUser.FirstName,
			LastName = inputUser.LastName,
			Email = inputUser.Email,
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		};

		var successfulResponse = new ApiResponse<User>()
		{
			Data = expectedUser,
			Success = true,
		};

		_userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
			.ReturnsAsync(successfulResponse);

		var result = await _userService.CreateAsync(inputUser);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result.Data.Should().BeEquivalentTo(expectedUser, options => options
		.Excluding(u => u.DateCreated)
		.Excluding(u => u.DateUpdated)
		.Excluding(u => u.Id));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<User>(u =>
			u.Id != Guid.Empty &&
			u.FirstName == inputUser.FirstName &&
			u.LastName == inputUser.LastName &&
			u.Email == inputUser.Email &&
			u.IsActive == true &&
			u.DateCreated != default &&
			u.DateUpdated != default)), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsUnsuccessfulResponse_WhenRepositoryThrowsException()
	{
		var user = GetUser();
		var exceptionMessage = "Database connection failed!";
		var expectedResponse = new ApiResponse<User>
		{
			Success = false,
			Message = $"Error in UserRepository CreateAsync: {exceptionMessage}"
		};

		_userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
			.ReturnsAsync(expectedResponse);

		var result = await _userService.CreateAsync(user);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(expectedResponse.Message);
	}

	[Fact]
	public async Task Delete_ReturnsSuccessfulResponse_WhenUserIsDeleted()
	{
		var user = GetUser();
		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var deleteResponse = new ApiResponse<User>
		{
			Success = true,
			Message = "Succesfully deleted!"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.DeleteAsync(user))
			.ReturnsAsync(deleteResponse);

		var result = await _userService.DeleteAsync(user.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be(deleteResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<User>(u => u.IsActive == false)), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsUnsuccessfulResponse_WhenUserDoesNotExist()
	{
		var userId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<User>()
		{
			Success = false,
			Message = $"User with Id {userId} doesn't exist"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
			.ReturnsAsync(getByIdResponse);

		var result = await _userService.DeleteAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(getByIdResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
		_userRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<User>()), Times.Never);
	}

	[Fact]
	public async Task Delete_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var user = GetUser();
		var exceptionMessage = "Database connection failed!";
		var getByIdResponse = new ApiResponse<User>()
		{
			Success = true,
			Data = user
		};

		var deleteResponse = new ApiResponse<User>()
		{
			Success = false,
			Message = $"Error in UserRepository DeleteAsync: {exceptionMessage}"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.DeleteAsync(user))
			.ReturnsAsync(deleteResponse);

		var result = await _userService.DeleteAsync(user.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(deleteResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task Update_ReturnsSuccessfulResponse_WhenUserIsUpdated()
	{
		var existingUser = GetUser();
		var updatedUser = new User { FirstName = "Ada", LastName = "Lovelace", Email = "alovelace@gmail.com" };
		var getByIdResponse = new ApiResponse<User>()
		{
			Success = true,
			Data = existingUser
		};

		var updateResponse = new ApiResponse<User>()
		{
			Success = true,
			Message = "Successfully updated!"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		User capturedUser = new User();
		_userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
			.Callback<User>(u => capturedUser = u)
			.ReturnsAsync(updateResponse);

		var testStartTime = DateTime.Now;

		var result = await _userService.UpdateAsync(existingUser.Id, updatedUser);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be(updateResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(existingUser.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
		capturedUser.FirstName.Should().Be("Ada");
		capturedUser.LastName.Should().Be("Lovelace");
		capturedUser.Email.Should().Be("alovelace@gmail.com");
		capturedUser.DateUpdated.Should().BeOnOrAfter(testStartTime);
	}

	[Fact]
	public async Task Update_ReturnsUnsuccessfulResponse_WhenUserDoesNotExist()
	{
		var userId = Guid.NewGuid();
		var updatedUser = new User { FirstName = "Ada", LastName = "Lovelace", Email = "alovelace@gmail.com" };
		var getByIdResponse = new ApiResponse<User>()
		{
			Success = false,
			Message = $"User with Id {userId} doesn't exist"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _userService.UpdateAsync(userId, updatedUser);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(getByIdResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
	}

	[Fact]
	public async Task Update_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var user = GetUser();
		var exceptionMessage = "Database connection failed!";
		var getByIdResponse = new ApiResponse<User>()
		{
			Success = true,
			Data = user
		};

		var updateResponse = new ApiResponse<User>()
		{
			Success = false,
			Message = $"Error in UserRepository UpdateAsync: {exceptionMessage}"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
			.ReturnsAsync(updateResponse);

		var result = await _userService.UpdateAsync(user.Id, user);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(updateResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsSuccessfulResponse_WhenShiftsExist()
	{
		var user = GetUser();
		var shifts = GetShifts();

		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var shiftsResponse = new ApiResponse<List<Shift>>
		{
			Success = true,
			Data = shifts
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.GetShiftsByUserIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(shiftsResponse);

		var result = await _userService.GetShiftsByUserIdAsync(user.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNullOrEmpty();
		result?.Data?.Count.Should().Be(2);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.GetShiftsByUserIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsUnsuccessfulResponse_WhenUserNotFound()
	{
		var userId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<User>
		{
			Success = false,
			Message = $"User with Id {userId} doesn't exist"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _userService.GetShiftsByUserIdAsync(userId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(getByIdResponse.Message);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_userRepositoryMock.Verify(repo => repo.GetShiftsByUserIdAsync(It.IsAny<Guid>()), Times.Never);
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsUnsuccessfulResponse_WhenShiftsNotFound()
	{
		var user = GetUser();
		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var shiftsResponse = new ApiResponse<List<Shift>>
		{
			Success = false,
			Message = "No shifts found!"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.GetShiftsByUserIdAsync(user.Id))
			.ReturnsAsync(shiftsResponse);

		var result = await _userService.GetShiftsByUserIdAsync(user.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(shiftsResponse.Message);
		result.Data.Should().BeNull();

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.GetShiftsByUserIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task GetShiftsByUserId_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var exceptionMessage = "Database connection failed!";
		var user = GetUser();
		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var shiftsResponse = new ApiResponse<List<Shift>>
		{
			Success = false,
			Message = $"Error in UserRepository GetShiftsByUserIdAsync: {exceptionMessage}"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.GetShiftsByUserIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(shiftsResponse);

		var result = await _userService.GetShiftsByUserIdAsync(user.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(shiftsResponse.Message);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.GetShiftsByUserIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsSuccessfulResponse_WhenShiftsAreUpdated()
	{
		var user = GetUser();
		var shiftsToUpdate = GetShifts();

		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var updateShiftsResponse = new ApiResponse<User>
		{
			Success = true,
			Message = "Successfully updated!"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate))
			.ReturnsAsync(updateShiftsResponse);

		var result = await _userService.UpdateShiftsAsync(user.Id, shiftsToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be(updateShiftsResponse.Message);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate), Times.Once);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsUnsuccessfulResponse_WhenNoChangesAreMade()
	{
		var user = GetUser();
		var shiftsToUpdate = GetShifts();

		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var updateShiftsResponse = new ApiResponse<User>
		{
			Success = false,
			Message = "No changes to update!"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate))
			.ReturnsAsync(updateShiftsResponse);

		var result = await _userService.UpdateShiftsAsync(user.Id, shiftsToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(updateShiftsResponse.Message);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate), Times.Once);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsUnsuccessfulResponse_WhenShiftsOverlap()
	{
		var user = GetUser();
		var shiftsToUpdate = GetShifts();

		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var updateShiftsResponse = new ApiResponse<User>
		{
			Success = false,
			Message = "Overlap with an already assigned shift!"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate))
			.ReturnsAsync(updateShiftsResponse);

		var result = await _userService.UpdateShiftsAsync(user.Id, shiftsToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(updateShiftsResponse.Message);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate), Times.Once);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsUnsuccessfulResponse_WhenUserDoesNotExist()
	{
		var userId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<User>
		{
			Success = false,
			Message = $"User with Id {userId} doesn't exist"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(getByIdResponse);

		var result = await _userService.UpdateShiftsAsync(userId, It.IsAny<List<Shift>>());

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(getByIdResponse.Message);
		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateShiftsAsync(It.IsAny<Guid>(), It.IsAny<List<Shift>>()), Times.Never);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var exceptionMessage = "Database connection failed!";
		var user = GetUser();
		var shiftsToUpdate = GetShifts();

		var getByIdResponse = new ApiResponse<User>
		{
			Success = true,
			Data = user
		};

		var updateResponse = new ApiResponse<User>
		{
			Success = false,
			Message = $"Error in UserRepository UpdateShiftsAsync: {exceptionMessage}"
		};

		_userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_userRepositoryMock.Setup(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate))
			.ReturnsAsync(updateResponse);

		var result = await _userService.UpdateShiftsAsync(user.Id, shiftsToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(updateResponse.Message);

		_userRepositoryMock.Verify(repo => repo.GetByIdAsync(user.Id), Times.Once);
		_userRepositoryMock.Verify(repo => repo.UpdateShiftsAsync(user.Id, shiftsToUpdate), Times.Once);
	}

	private static List<Shift> GetShifts()
	{
		var shifts = new List<Shift>();
		shifts.AddRange(new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddHours(8),
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		},
		new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddHours(7),
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		});
		return shifts;
	}

	private static User GetUser()
	{
		return new User()
		{
			Id = Guid.NewGuid(),
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com",
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now
		};
	}

	private static List<User> GetUsers()
	{
		var users = new List<User>();
		users.AddRange(new User
		{
			Id = Guid.NewGuid(),
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com",
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		},
		new User
		{
			Id = Guid.NewGuid(),
			FirstName = "Ada",
			LastName = "Lovelace",
			Email = "alovelace@gmail.com",
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		});

		return users;
	}
}
