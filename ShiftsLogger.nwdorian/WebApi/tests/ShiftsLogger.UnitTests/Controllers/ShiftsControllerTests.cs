using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.Controllers;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.UnitTests.Controllers;
public class ShiftsControllerTests
{
	private readonly ShiftsController _shiftsController;
	private readonly Mock<IShiftService> _shiftsServiceMock;
	private readonly IMapper _mapper;
	public ShiftsControllerTests()
	{
		_shiftsServiceMock = new Mock<IShiftService>();
		_mapper = SetUpAutomapper();
		_shiftsController = new ShiftsController(_shiftsServiceMock.Object, _mapper);
	}

	[Fact]
	public async Task GetAll_ReturnsOkWithListOfShifts_WhenDataExists()
	{
		var shifts = GetShifts();
		var getAllResponse = new ApiResponse<List<Shift>>
		{
			Data = shifts,
			Success = true
		};
		_shiftsServiceMock.Setup(s => s.GetAllAsync())
			.ReturnsAsync(getAllResponse);

		var result = await _shiftsController.GetAll();

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>();
		result.As<OkObjectResult>().Value.Should().BeOfType<List<ShiftRead>>()
			.Which.Should().HaveCount(2);
		_shiftsServiceMock.Verify(s => s.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetAll_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var getAllResponse = new ApiResponse<List<Shift>>
		{
			Data = null,
			Success = false,
			Message = "No shifts found!"
		};
		_shiftsServiceMock.Setup(s => s.GetAllAsync())
			.ReturnsAsync(getAllResponse);

		var result = await _shiftsController.GetAll();

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(getAllResponse.Message);
		_shiftsServiceMock.Verify(s => s.GetAllAsync(), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsOkWithShift_WhenShiftExists()
	{
		var shift = GetShifts().First();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true
		};
		_shiftsServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _shiftsController.GetById(shift.Id);

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>()
			.Which.Value.Should().BeOfType<ShiftRead>();
		_shiftsServiceMock.Verify(s => s.GetByIdAsync(shift.Id), Times.Once);
	}

	[Fact]
	public async Task GetById_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var shiftId = Guid.NewGuid();
		var getByIdResponse = new ApiResponse<Shift>
		{
			Data = null,
			Success = false,
			Message = "Shift not found"
		};
		_shiftsServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getByIdResponse);

		var result = await _shiftsController.GetById(shiftId);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(getByIdResponse.Message);
		_shiftsServiceMock.Verify(s => s.GetByIdAsync(shiftId), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsCreatedAtActionWithCreatedShift_WhenShiftIsCreated()
	{
		var shiftCreate = new ShiftCreate
		{
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(10),
		};
		var shift = _mapper.Map<Shift>(shiftCreate);
		var createResponse = new ApiResponse<Shift>
		{
			Data = shift,
			Success = true,
		};
		_shiftsServiceMock.Setup(s => s.CreateAsync(It.IsAny<Shift>()))
			.ReturnsAsync(createResponse);

		var result = await _shiftsController.Create(shiftCreate);

		result.Should().NotBeNull();
		result.Should().BeOfType<CreatedAtActionResult>()
			.Which.Value.Should().BeOfType<ShiftRead>();
		_shiftsServiceMock.Verify(s => s.CreateAsync(It.IsAny<Shift>()), Times.Once);
	}

	[Fact]
	public async Task Create_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var shiftCreate = new ShiftCreate
		{
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(10)
		};
		var shift = _mapper.Map<Shift>(shiftCreate);
		var createResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = "Error in ShiftsRepository"
		};
		_shiftsServiceMock.Setup(s => s.CreateAsync(It.IsAny<Shift>()))
			.ReturnsAsync(createResponse);

		var result = await _shiftsController.Create(shiftCreate);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(createResponse.Message);
		_shiftsServiceMock.Verify(s => s.CreateAsync(It.IsAny<Shift>()), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsNoContent_WhenShiftIsDeleted()
	{
		var shiftId = Guid.NewGuid();
		var deleteResponse = new ApiResponse<Shift>
		{
			Success = true,
			Message = "Deleted"
		};
		_shiftsServiceMock.Setup(s => s.DeleteAsync(It.IsAny<Guid>()))
			.ReturnsAsync(deleteResponse);

		var result = await _shiftsController.Delete(shiftId);

		result.Should().NotBeNull();
		result.Should().BeOfType<NoContentResult>();
		_shiftsServiceMock.Verify(s => s.DeleteAsync(shiftId), Times.Once);
	}

	[Fact]
	public async Task Delete_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var shiftId = Guid.NewGuid();
		var deleteResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = "Error in ShiftsRepository"
		};
		_shiftsServiceMock.Setup(s => s.DeleteAsync(It.IsAny<Guid>()))
			.ReturnsAsync(deleteResponse);

		var result = await _shiftsController.Delete(shiftId);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(deleteResponse.Message);
		_shiftsServiceMock.Verify(s => s.DeleteAsync(shiftId), Times.Once);
	}

	[Fact]
	public async Task Update_ReturnsNoContent_WhenShiftIsUpdated()
	{
		var shiftId = Guid.NewGuid();
		var shiftUpdate = new ShiftUpdate
		{
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(10)
		};
		var updateResponse = new ApiResponse<Shift>
		{
			Success = true,
			Message = "Updated"
		};
		_shiftsServiceMock.Setup(s => s.UpdateAsync(shiftId, It.IsAny<Shift>()))
			.ReturnsAsync(updateResponse);

		var result = await _shiftsController.Update(shiftId, shiftUpdate);

		result.Should().NotBeNull();
		result.Should().BeOfType<NoContentResult>();
		_shiftsServiceMock.Verify(s => s.UpdateAsync(shiftId, It.IsAny<Shift>()), Times.Once);
	}

	[Fact]
	public async Task Update_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var shiftId = Guid.NewGuid();
		var shiftUpdate = new ShiftUpdate
		{
			StartTime = DateTime.Now.AddHours(2),
			EndTime = DateTime.Now.AddHours(10)
		};
		var updateResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = "Error in ShiftsRepository"
		};
		_shiftsServiceMock.Setup(s => s.UpdateAsync(shiftId, It.IsAny<Shift>()))
			.ReturnsAsync(updateResponse);

		var result = await _shiftsController.Update(shiftId, shiftUpdate);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(updateResponse.Message);
		_shiftsServiceMock.Verify(s => s.UpdateAsync(shiftId, It.IsAny<Shift>()), Times.Once);
	}

	[Fact]
	public async Task UpdateUsers_ReturnsNoContent_WhenUsersAreUpdated()
	{
		var shiftId = Guid.NewGuid();
		var users = new List<UserRead>
		{
			new UserRead
			{
				Id = Guid.NewGuid(),
				FirstName = "John",
				LastName = "Doe",
				Email = "jdoe@gmail.com"
			},
			new UserRead
			{
				Id = Guid.NewGuid(),
				FirstName = "Ada",
				LastName = "Lovelace",
				Email = "alovelace@gmail.com"
			}
		};
		var updateUsersResponse = new ApiResponse<Shift>
		{
			Success = true,
			Message = "Users updated"
		};
		_shiftsServiceMock.Setup(s => s.UpdateUsersAsync(shiftId, It.IsAny<List<User>>()))
			.ReturnsAsync(updateUsersResponse);

		var result = await _shiftsController.UpdateUsers(shiftId, users);

		result.Should().NotBeNull();
		result.Should().BeOfType<NoContentResult>();
		_shiftsServiceMock.Verify(s => s.UpdateUsersAsync(shiftId, It.IsAny<List<User>>()), Times.Once);
	}

	[Fact]
	public async Task UpdateUsers_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var shiftId = Guid.NewGuid();
		var users = new List<UserRead>
		{
			new UserRead
			{
				Id = Guid.NewGuid(),
				FirstName = "John",
				LastName = "Doe",
				Email = "jdoe@gmail.com"
			},
			new UserRead
			{
				Id = Guid.NewGuid(),
				FirstName = "Ada",
				LastName = "Lovelace",
				Email = "alovelace@gmail.com"
			}
		};
		var updateUsersResponse = new ApiResponse<Shift>
		{
			Success = false,
			Message = "Error in ShiftsRepository"
		};
		_shiftsServiceMock.Setup(s => s.UpdateUsersAsync(shiftId, It.IsAny<List<User>>()))
			.ReturnsAsync(updateUsersResponse);

		var result = await _shiftsController.UpdateUsers(shiftId, users);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(updateUsersResponse.Message);
		_shiftsServiceMock.Verify(s => s.UpdateUsersAsync(shiftId, It.IsAny<List<User>>()), Times.Once);
	}

	[Fact]
	public async Task GetUsers_ReturnsOkWithListOfUsers_WhenDataExists()
	{
		var shiftId = Guid.NewGuid();
		var users = new List<User>()
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
			}
		};
		var getUsersResponse = new ApiResponse<List<User>>
		{
			Data = users,
			Success = true
		};
		_shiftsServiceMock.Setup(s => s.GetUsersByShiftIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getUsersResponse);

		var result = await _shiftsController.GetUsers(shiftId);

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>()
			.Which.Value.Should().BeOfType<List<UserRead>>()
			.Which.Should().HaveCount(2);
		_shiftsServiceMock.Verify(s => s.GetUsersByShiftIdAsync(shiftId), Times.Once);
	}

	[Fact]
	public async Task GetUsers_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var shiftId = Guid.NewGuid();
		var getUsersResponse = new ApiResponse<List<User>>
		{
			Success = false,
			Message = "Error in ShiftsRepository"
		};
		_shiftsServiceMock.Setup(s => s.GetUsersByShiftIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(getUsersResponse);

		var result = await _shiftsController.GetUsers(shiftId);

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(getUsersResponse.Message);
		_shiftsServiceMock.Verify(s => s.GetUsersByShiftIdAsync(shiftId), Times.Once);
	}

	private List<Shift> GetShifts()
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
