namespace Buutyful.ShiftsLogger.Domain.Tests;

[TestClass]
public class ShiftTests
{
    [TestMethod]
    public void Create_ValidInput_ReturnsShiftInstance()
    {
        // Arrange
        Guid workerId = Guid.NewGuid();
        DateTime shiftDay = DateTime.Now.Date;
        DateTime startAt = DateTime.Now;
        DateTime endAt = DateTime.Now.AddHours(8);

        // Act
        Shift shift = Shift.Create(workerId, shiftDay, startAt, endAt);

        // Assert
        Assert.IsNotNull(shift);
        Assert.IsInstanceOfType(shift, typeof(Shift));
        Assert.AreEqual(workerId, shift.WorkerId);
        Assert.AreEqual(shiftDay, shift.ShiftDay);
        Assert.AreEqual(startAt, shift.StartAt);
        Assert.AreEqual(endAt, shift.EndAt);
        Assert.AreEqual(startAt - endAt, shift.Duration);
        Assert.AreNotEqual(Guid.Empty, shift.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Create_InvalidTimeRange_ThrowsArgumentException()
    {
        // Arrange
        Guid workerId = Guid.NewGuid();
        DateTime shiftDay = DateTime.Now.Date;
        DateTime startAt = DateTime.Now.AddHours(8);
        DateTime endAt = DateTime.Now;

        // Act
        Shift shift = Shift.Create(workerId, shiftDay, startAt, endAt);

        // Assert
        // Expecting ArgumentException to be thrown
    }
}
