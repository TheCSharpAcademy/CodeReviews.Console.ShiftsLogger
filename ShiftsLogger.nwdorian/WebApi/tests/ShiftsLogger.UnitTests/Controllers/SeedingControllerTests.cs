using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.Controllers;

namespace ShiftsLogger.UnitTests.Controllers;
public class SeedingControllerTests
{
	private readonly SeedingController _seedingController;
	private readonly Mock<ISeedingService> _seedingServiceMock;
	public SeedingControllerTests()
	{
		_seedingServiceMock = new Mock<ISeedingService>();
		_seedingController = new SeedingController(_seedingServiceMock.Object);
	}

	[Fact]
	public async Task SeedData_ReturnsOk_WhenDatabaseIsSeeded()
	{
		var seedingResponse = new ApiResponse<string>
		{
			Success = true,
			Message = "Database seeded"
		};
		_seedingServiceMock.Setup(s => s.SeedDataAsync())
			.ReturnsAsync(seedingResponse);

		var result = await _seedingController.SeedData();

		result.Should().NotBeNull();
		result.Should().BeOfType<OkObjectResult>()
			.Which.Value.Should().Be(seedingResponse.Message);
		_seedingServiceMock.Verify(s => s.SeedDataAsync(), Times.Once);
	}

	[Fact]
	public async Task SeedData_ReturnsBadRequest_WhenServiceResponseIsUnsuccessful()
	{
		var seedingResponse = new ApiResponse<string>
		{
			Success = false,
			Message = "Records already exist"
		};
		_seedingServiceMock.Setup(s => s.SeedDataAsync())
			.ReturnsAsync(seedingResponse);

		var result = await _seedingController.SeedData();

		result.Should().NotBeNull();
		result.Should().BeOfType<BadRequestObjectResult>()
			.Which.Value.Should().Be(seedingResponse.Message);
		_seedingServiceMock.Verify(s => s.SeedDataAsync(), Times.Once);
	}
}
