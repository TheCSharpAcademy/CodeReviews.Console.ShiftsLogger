using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.Controllers;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.UnitTests.Controllers;
public class UsersControllerTests
{
	private readonly UsersController _usersController;
	private readonly Mock<IUserService> _userServiceMock;
	private readonly IMapper _mapper;
	public UsersControllerTests()
	{
		_userServiceMock = new Mock<IUserService>();
		_mapper = SetUpAutomapper();
		_usersController = new UsersController(_userServiceMock.Object, _mapper);
	}

	[Fact]
	public async Task GetAll_ReturnsOkWithListOfUsers_WhenServiceDataExists()
	{
		var users = GetUsers();
		var getAllResponse = new ApiResponse<List<User>>
		{
			Data = users,
			Success = true
		};
		_userServiceMock.Setup(s => s.GetAllAsync())
			.ReturnsAsync(getAllResponse);

		var result = await _usersController.GetAll();

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>()
			.Which.StatusCode.Should().Be(200);
		result.As<OkObjectResult>().Value.Should().BeOfType<List<UserRead>>()
			.Which.Should().HaveCount(2);
		_userServiceMock.Verify(s => s.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetAll_ReturnsBadRequest_WhenServiceRespsonseIsUnsuccessful()
	{
		var getAllResponse = new ApiResponse<List<User>>
		{
			Data = null,
			Success = false,
			Message = "No users found!"
		};
		_userServiceMock.Setup(s => s.GetAllAsync())
			.ReturnsAsync(getAllResponse);

		var result = await _usersController.GetAll();

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(getAllResponse.Message);
		_userServiceMock.Verify(s => s.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsOkWithUser_WhenUserExists()
	{
		var user = GetUsers().First();
		var getByIdResponse = new ApiResponse<User>
		{
			Data = user,
			Success = true,
		};
		var userRead = _mapper.Map<UserRead>(user);
		_userServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _usersController.GetById(user.Id);

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>()
			.Which.StatusCode.Should().Be(200);
		result.As<OkObjectResult>().Value.Should().BeOfType<UserRead>()
			.And.BeEquivalentTo(userRead);
		_userServiceMock.Verify(s => s.GetByIdAsync(user.Id), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var userId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<User>
		{
			Data = null,
			Success = false,
			Message = "User doesn't exist!"
		};
		_userServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _usersController.GetById(userId);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(getByIdResponse.Message);
		_userServiceMock.Verify(s => s.GetByIdAsync(userId), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsCreatedAtActionWithCreatedUser_WhenUserIsCreated()
	{
		var userCreate = new UserCreate
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com"
		};
		var user = _mapper.Map<User>(userCreate);
		var createResponse = new ApiResponse<User>
		{
			Data = user,
			Success = true
		};
		_userServiceMock.Setup(s => s.CreateAsync(It.IsAny<User>()))
			.ReturnsAsync(createResponse);

		var result = await _usersController.Create(userCreate);

		result.Should().NotBeNull();
		result.Should().BeOfType<CreatedAtActionResult>()
			.Which.StatusCode.Should().Be(201);
		result.As<CreatedAtActionResult>().Value.Should().BeOfType<UserRead>();
		_userServiceMock.Verify(s => s.CreateAsync(It.IsAny<User>()), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var userCreate = new UserCreate
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com"
		};
		var createResponse = new ApiResponse<User>
		{
			Success = false,
			Message = "Error in UserRepository"
		};
		_userServiceMock.Setup(s => s.CreateAsync(It.IsAny<User>()))
			.ReturnsAsync(createResponse);

		var result = await _usersController.Create(userCreate);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(createResponse.Message);
		_userServiceMock.Verify(s => s.CreateAsync(It.IsAny<User>()), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsNoContent_WhenUserIsDeleted()
	{
		var userId = Guid.NewGuid();
		var deleteResponse = new ApiResponse<User>
		{
			Success = true,
			Message = "Deleted!"
		};
		_userServiceMock.Setup(s => s.DeleteAsync(userId))
			.ReturnsAsync(deleteResponse);

		var result = await _usersController.Delete(userId);

		result.Should().NotBeNull();
		result.Should().BeOfType<NoContentResult>();
		_userServiceMock.Verify(s => s.DeleteAsync(userId), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var userId = Guid.NewGuid();
		var deleteResponse = new ApiResponse<User>
		{
			Success = false,
			Message = "Error in UserRepository"
		};
		_userServiceMock.Setup(s => s.DeleteAsync(userId))
			.ReturnsAsync(deleteResponse);

		var result = await _usersController.Delete(userId);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(deleteResponse.Message);
		_userServiceMock.Verify(s => s.DeleteAsync(userId), Times.Once);
	}

	[Fact]
	public async Task Update_ReturnsNoContent_WhenUserIsUpdated()
	{
		var userId = Guid.NewGuid();
		var userUpdate = new UserUpdate
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com"
		};
		var updateResponse = new ApiResponse<User>
		{
			Success = true,
			Message = "Updated"
		};
		_userServiceMock.Setup(s => s.UpdateAsync(userId, It.IsAny<User>()))
			.ReturnsAsync(updateResponse);

		var result = await _usersController.Update(userId, userUpdate);

		result.Should().NotBeNull();
		result.Should().BeOfType<NoContentResult>();
		_userServiceMock.Verify(s => s.UpdateAsync(userId, It.IsAny<User>()), Times.Once);
	}

	[Fact]
	public async Task Update_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var userId = Guid.NewGuid();
		var userUpdate = new UserUpdate
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "jdoe@gmail.com"
		};
		var updateResponse = new ApiResponse<User>
		{
			Success = false,
			Message = "Error in UserRepository"
		};
		_userServiceMock.Setup(s => s.UpdateAsync(userId, It.IsAny<User>()))
			.ReturnsAsync(updateResponse);

		var result = await _usersController.Update(userId, userUpdate);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(updateResponse.Message);
		_userServiceMock.Verify(s => s.UpdateAsync(userId, It.IsAny<User>()), Times.Once);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsNoContent_WhenShiftsAreUpdated()
	{
		var userId = Guid.NewGuid();
		var shiftsRead = new List<ShiftRead>
		{
			new ShiftRead
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Now,
				EndTime = DateTime.Now.AddHours(8)
			},
			new ShiftRead
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Now,
				EndTime = DateTime.Now.AddHours(7)
			}
		};
		var updateShiftsResponse = new ApiResponse<User>
		{
			Success = true,
			Message = "Shifts updated"
		};
		_userServiceMock.Setup(s => s.UpdateShiftsAsync(userId, It.IsAny<List<Shift>>()))
			.ReturnsAsync(updateShiftsResponse);

		var result = await _usersController.UpdateShifts(userId, shiftsRead);

		result.Should().NotBeNull();
		result.Should().BeOfType<NoContentResult>();
		_userServiceMock.Verify(s => s.UpdateShiftsAsync(userId, It.IsAny<List<Shift>>()), Times.Once);
	}

	[Fact]
	public async Task UpdateShifts_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var userId = Guid.NewGuid();
		var shiftsRead = new List<ShiftRead>
		{
			new ShiftRead
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Now,
				EndTime = DateTime.Now.AddHours(8)
			},
			new ShiftRead
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Now,
				EndTime = DateTime.Now.AddHours(7)
			}
		};
		var updateShiftsResponse = new ApiResponse<User>
		{
			Success = false,
			Message = "Error in UserRepository"
		};
		_userServiceMock.Setup(s => s.UpdateShiftsAsync(userId, It.IsAny<List<Shift>>()))
			.ReturnsAsync(updateShiftsResponse);

		var result = await _usersController.UpdateShifts(userId, shiftsRead);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(updateShiftsResponse.Message);
		_userServiceMock.Verify(s => s.UpdateShiftsAsync(userId, It.IsAny<List<Shift>>()), Times.Once);
	}

	[Fact]
	public async Task GetShifts_ReturnsListOfShifts_WhenDataExist()
	{
		var userId = Guid.NewGuid();
		var shifts = new List<Shift>
		{
			new Shift
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
			}
		};
		var getShiftsResponse = new ApiResponse<List<Shift>>
		{
			Data = shifts,
			Success = true
		};

		_userServiceMock.Setup(s => s.GetShiftsByUserIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getShiftsResponse);

		var result = await _usersController.GetShifts(userId);

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>();
		result.As<OkObjectResult>().Value.Should().BeOfType<List<ShiftRead>>()
			.Which.Should().HaveCount(2);
		_userServiceMock.Verify(s => s.GetShiftsByUserIdAsync(userId), Times.Once);
	}

	[Fact]
	public async Task GetShifts_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var userId = Guid.NewGuid();
		var getShiftsResponse = new ApiResponse<List<Shift>>
		{
			Success = false,
			Message = "Error in UserRepository"
		};

		_userServiceMock.Setup(s => s.GetShiftsByUserIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getShiftsResponse);

		var result = await _usersController.GetShifts(userId);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.StatusCode.Should().Be(400)
			.And.HaveValue(getShiftsResponse.Message);
		_userServiceMock.Verify(s => s.GetShiftsByUserIdAsync(userId), Times.Once);
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

	private static IMapper SetUpAutomapper()
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new ControllerProfile());
		});
		var mapper = config.CreateMapper();
		return mapper;
	}
}


