using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service;

namespace ShiftsLogger.UnitTests.Services;
public class ShiftServiceTests
{
	private readonly string _exceptionMessage = "Database connection failed!";
	private readonly ShiftService _shiftService;
	private readonly Mock<IShiftRepository> _shiftRepositoryMock;
	public ShiftServiceTests()
	{
		_shiftRepositoryMock = new Mock<IShiftRepository>();
		_shiftService = new ShiftService(_shiftRepositoryMock.Object);
	}

	[Fact]
	public async Task GetAll_ReturnsListOfUsers_WhenRepositoryReturnsData()
	{
		var shifts = GetShifts();
		var shiftsResponse = new ApiResponse<List<Shift>>
		{
			Success = true,
			Data = shifts
		};

		_shiftRepositoryMock.Setup(repo => repo.GetAllAsync())
			.ReturnsAsync(shiftsResponse);

		var result = await _shiftService.GetAllAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result.Data.Should().HaveCount(shifts.Count);
		_shiftRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once());
	}

	[Fact]
	public async Task GetAll_ReturnsUnsuccessfulResponse_WhenRepositoryReturnsNoData()
	{
		var shiftsResponse = new ApiResponse<List<Shift>>
		{
			Data = null,
			Success = false,
			Message = "No shifts found!"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetAllAsync())
			.ReturnsAsync(shiftsResponse);

		var result = await _shiftService.GetAllAsync();

		result.Should().NotBeNull();
		result.Data.Should().BeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(shiftsResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once());
	}

	[Fact]
	public async Task GetAll_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var shiftsResponse = new ApiResponse<List<Shift>>
		{
			Data = null,
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.GetAllAsync())
			.ReturnsAsync(shiftsResponse);

		var result = await _shiftService.GetAllAsync();

		result.Should().NotBeNull();
		result.Data.Should().BeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(_exceptionMessage);
		_shiftRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once());
	}

	[Fact]
	public async Task GetById_ReturnsShift_WhenShiftExists()
	{
		var shift = GetShift();
		var shiftResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(shiftResponse);

		var result = await _shiftService.GetByIdAsync(shift.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result.Data.Should().BeEquivalentTo(shift);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(shift.Id), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var shiftId = Guid.NewGuid();
		var shiftResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = $"Shift with Id {shiftId} doesn't exist"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(shiftResponse);

		var result = await _shiftService.GetByIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(shiftResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(shiftId), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var shiftId = Guid.NewGuid();
		var shiftResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(shiftResponse);

		var result = await _shiftService.GetByIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(shiftResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(shiftId), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsSuccessfulResponse_WhenShiftIsCreated()
	{
		var inputUser = new Shift
		{
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(2),
		};

		var expectedUser = new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = inputUser.StartTime,
			EndTime = inputUser.EndTime,
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now
		};

		var createResponse = new ApiResponse<Shift>
		{
			Data = expectedUser,
			Success = true
		};

		_shiftRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Shift>()))
			.ReturnsAsync(createResponse);

		var result = await _shiftService.CreateAsync(inputUser);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result.Data.Should().BeEquivalentTo(expectedUser, options => options
			.Excluding(s => s.Id)
			.Excluding(s => s.DateCreated)
			.Excluding(s => s.DateUpdated));

		_shiftRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Shift>(s =>
			s.Id != Guid.Empty &&
			s.StartTime == inputUser.StartTime &&
			s.EndTime == inputUser.EndTime &&
			s.IsActive == true &&
			s.DateCreated != default &&
			s.DateUpdated != default)), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var shift = GetShift();
		var createResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Shift>()))
			.ReturnsAsync(createResponse);

		var result = await _shiftService.CreateAsync(shift);

		result.Should().NotBeNull();
		result.Data.Should().BeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(_exceptionMessage);
		_shiftRepositoryMock.Verify(repo => repo.CreateAsync(shift), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsSuccessfulResponse_WhenShiftIsDeleted()
	{
		var shift = GetShift();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		var deleteResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = true,
			Message = "Succesfully deleted!"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_shiftRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Shift>()))
			.ReturnsAsync(deleteResponse);

		var result = await _shiftService.DeleteAsync(shift.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().BeNull();
		result.Message.Should().Be(deleteResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(shift.Id), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<Shift>(s => s.IsActive == false)), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var shiftId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = $"Shift with Id {shiftId} doesn't exist"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _shiftService.DeleteAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(getByIdResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(shiftId), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Shift>()), Times.Never);
	}

	[Fact]
	public async Task Delete_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var shift = GetShift();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		var deleteResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		_shiftRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Shift>()))
			.ReturnsAsync(deleteResponse);

		var result = await _shiftService.DeleteAsync(shift.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Data.Should().BeNull();
		result.Message.Should().Be(deleteResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(shift.Id), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.DeleteAsync(shift), Times.Once);
	}

	[Fact]
	public async Task Update_ReturnsSuccessfulResponse_WhenShiftIsUpdated()
	{
		var existingShift = GetShift();
		var updatedShift = new Shift
		{
			StartTime = DateTime.Now.AddHours(4),
			EndTime = DateTime.Now.AddHours(12),
		};

		var getByIdResponse = new ApiResponse<Shift>
		{
			Success = true,
			Data = existingShift
		};

		var updateResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = true,
			Message = "Successfully updated!"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var capturedShift = new Shift();
		_shiftRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Shift>()))
			.Callback<Shift>(s => capturedShift = s)
			.ReturnsAsync(updateResponse);

		var testStartTime = DateTime.Now;

		var result = await _shiftService.UpdateAsync(existingShift.Id, updatedShift);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be(updateResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Shift>()), Times.Once);
		capturedShift.StartTime.Should().Be(updatedShift.StartTime);
		capturedShift.EndTime.Should().Be(updatedShift.EndTime);
		capturedShift.DateUpdated.Should().BeOnOrAfter(testStartTime);
	}

	[Fact]
	public async Task Update_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var shiftId = Guid.NewGuid();
		var updatedShift = new Shift
		{
			StartTime = DateTime.Now.AddHours(4),
			EndTime = DateTime.Now.AddHours(12),
		};

		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = $"Shift with Id {shiftId} doesn't exist"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _shiftService.UpdateAsync(shiftId, updatedShift);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(getByIdResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Shift>()), Times.Never);
	}

	[Fact]
	public async Task Update_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var existingShift = GetShift();
		var updatedShift = new Shift
		{
			StartTime = DateTime.Now.AddHours(4),
			EndTime = DateTime.Now.AddHours(12),
		};

		var getByIdResponse = new ApiResponse<Shift>
		{
			Success = true,
			Data = existingShift
		};

		var updateResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Shift>()))
			.ReturnsAsync(updateResponse);

		var result = await _shiftService.UpdateAsync(existingShift.Id, updatedShift);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(_exceptionMessage);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Shift>()), Times.Once);
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsListOfUsers_WhenDataExists()
	{
		var shift = GetShift();
		var users = GetUsers();

		var getByIdResponse = new ApiResponse<Shift>
		{
			Success = true,
			Data = shift
		};

		var usersResponse = new ApiResponse<List<User>>
		{
			Success = true,
			Data = users
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(usersResponse);

		var result = await _shiftService.GetUsersByShiftIdAsync(shift.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Data.Should().NotBeNull();
		result?.Data?.Count.Should().Be(2);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()), Times.Once);
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsUnsuccessfulResponse_WhenRepositoryReturnsNoData()
	{
		var shift = GetShift();

		var getByIdResponse = new ApiResponse<Shift>
		{
			Success = true,
			Data = shift
		};

		var usersResponse = new ApiResponse<List<User>>
		{
			Data = null,
			Success = false,
			Message = "No users found!"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(usersResponse);

		var result = await _shiftService.GetUsersByShiftIdAsync(shift.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(usersResponse.Message);
		result.Data.Should().BeNull();
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()), Times.Once);
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var shiftId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = $"Shift with Id {shiftId} doesn't exist"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _shiftService.GetUsersByShiftIdAsync(shiftId);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(getByIdResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()), Times.Never);
	}

	[Fact]
	public async Task GetUsersByShiftId_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var shift = GetShift();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		var usersResponse = new ApiResponse<List<User>>
		{
			Data = null,
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(usersResponse);

		var result = await _shiftService.GetUsersByShiftIdAsync(shift.Id);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(_exceptionMessage);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.GetUsersByShiftIdAsync(It.IsAny<Guid>()), Times.Once);
	}

	[Fact]
	public async Task UpdateUsers_ReturnsSuccessfulResponse_WhenUsersAreUpdated()
	{
		var shift = GetShift();
		var usersToUpdate = GetUsers();

		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		var updateUsersResponse = new ApiResponse<Shift>
		{
			Success = true,
			Message = "Successfully updated!"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()))
			.ReturnsAsync(updateUsersResponse);

		var result = await _shiftService.UpdateUsersAsync(shift.Id, usersToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be(updateUsersResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()), Times.Once);
	}

	[Fact]
	public async Task UpdateUsers_ReturnsUnsuccessfulResponse_WhenNoChangesAreMade()
	{
		var shift = GetShift();
		var usersToUpdate = GetUsers();

		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		var updateUsersResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = "No changes to update!"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()))
			.ReturnsAsync(updateUsersResponse);

		var result = await _shiftService.UpdateUsersAsync(shift.Id, usersToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(updateUsersResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()), Times.Once);
	}

	[Fact]
	public async Task UpdateUsers_ReturnsUnsuccessfulResponse_WhenShiftDoesNotExist()
	{
		var shiftId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = $"Shift with Id {shiftId} doesn't exist"
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _shiftService.UpdateUsersAsync(shiftId, It.IsAny<List<User>>());

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(getByIdResponse.Message);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()), Times.Never);
	}

	[Fact]
	public async Task UpdateUsers_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var shift = GetShift();
		var usersToUpdate = GetUsers();

		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};

		var updateUsersResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = _exceptionMessage
		};

		_shiftRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);
		_shiftRepositoryMock.Setup(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()))
			.ReturnsAsync(updateUsersResponse);

		var result = await _shiftService.UpdateUsersAsync(shift.Id, usersToUpdate);

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(_exceptionMessage);
		_shiftRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
		_shiftRepositoryMock.Verify(repo => repo.UpdateUsersAsync(It.IsAny<Guid>(), It.IsAny<List<User>>()), Times.Once);
	}

	private static List<Shift> GetShifts()
	{
		var shifts = new List<Shift>();
		shifts.AddRange(new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(10),
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		},
		new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now.AddHours(4),
			EndTime = DateTime.Now.AddHours(12),
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
		});
		return shifts;
	}

	private static Shift GetShift()
	{
		return new Shift
		{
			Id = Guid.NewGuid(),
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(10),
			IsActive = true,
			DateCreated = DateTime.Now,
			DateUpdated = DateTime.Now,
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
