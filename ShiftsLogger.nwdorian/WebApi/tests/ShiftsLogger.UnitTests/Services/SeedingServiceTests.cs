using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service;

namespace ShiftsLogger.UnitTests.Services;
public class SeedingServiceTests
{
	private readonly SeedingService _seedingService;
	private readonly Mock<ISeedingRepository> _seedingRepositoryMock;
	public SeedingServiceTests()
	{
		_seedingRepositoryMock = new Mock<ISeedingRepository>();
		_seedingService = new SeedingService(_seedingRepositoryMock.Object);
	}

	[Fact]
	public async Task SeedData_ReturnsSuccessfulResponse_WhenDataIsSeededSuccessfully()
	{
		var seedingResponse = new ApiResponse<string>
		{
			Success = true,
			Message = "Database seeded successfully!"
		};

		_seedingRepositoryMock.Setup(repo => repo.RecordsExistAsync())
			.ReturnsAsync(false);
		_seedingRepositoryMock.Setup(repo => repo.AddUsersAndShiftsAsync(It.IsAny<List<User>>(), It.IsAny<List<Shift>>()))
			.ReturnsAsync(seedingResponse);

		var result = await _seedingService.SeedDataAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeTrue();
		result.Message.Should().Be(seedingResponse.Message);
		_seedingRepositoryMock.Verify(repo => repo.RecordsExistAsync(), Times.Once);
		_seedingRepositoryMock.Verify(repo => repo.AddUsersAndShiftsAsync(It.IsAny<List<User>>(), It.IsAny<List<Shift>>()), Times.Once);
	}

	[Fact]
	public async Task SeedData_ReturnsUnsuccessfulResponse_WhenRecordsExist()
	{
		_seedingRepositoryMock.Setup(repo => repo.RecordsExistAsync())
			.ReturnsAsync(true);

		var result = await _seedingService.SeedDataAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be("Records already exist in the database!");
		_seedingRepositoryMock.Verify(repo => repo.RecordsExistAsync(), Times.Once);
		_seedingRepositoryMock.Verify(repo => repo.AddUsersAndShiftsAsync(It.IsAny<List<User>>(), It.IsAny<List<Shift>>()), Times.Never);
	}

	[Fact]
	public async Task SeedData_ReturnsFailedResponse_WhenRepositoryThrowsException()
	{
		var seedingResponse = new ApiResponse<string>
		{
			Success = false,
			Message = "Database connection failed!"
		};

		_seedingRepositoryMock.Setup(repo => repo.RecordsExistAsync())
			.ReturnsAsync(false);
		_seedingRepositoryMock.Setup(repo => repo.AddUsersAndShiftsAsync(It.IsAny<List<User>>(), It.IsAny<List<Shift>>()))
			.ReturnsAsync(seedingResponse);

		var result = await _seedingService.SeedDataAsync();

		result.Should().NotBeNull();
		result.Success.Should().BeFalse();
		result.Message.Should().Be(seedingResponse.Message);
		_seedingRepositoryMock.Verify(repo => repo.RecordsExistAsync(), Times.Once);
		_seedingRepositoryMock.Verify(repo => repo.AddUsersAndShiftsAsync(It.IsAny<List<User>>(), It.IsAny<List<Shift>>()), Times.Once);
	}
}
