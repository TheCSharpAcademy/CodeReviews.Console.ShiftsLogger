namespace Buutyful.ShiftsLogger.Tests;
using Buutyful.ShiftsLogger.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class WorkerTests
{
    [TestMethod]
    public void Create_ValidInput_ReturnsWorkerInstance()
    {
        // Arrange
        string name = "John Doe";
        Role role = Role.Manager;

        // Act
        Worker worker = Worker.Create(name, role);

        // Assert
        Assert.IsNotNull(worker);
        Assert.IsInstanceOfType(worker, typeof(Worker));
        Assert.AreEqual(name, worker.Name);
        Assert.AreEqual(role, worker.Role);
        Assert.AreNotEqual(Guid.Empty, worker.Id);
    }

    [TestMethod]
    public void CreateWithId_ValidInput_ReturnsWorkerInstanceWithSpecifiedId()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Jane Doe";
        Role role = Role.Employee;

        // Act
        Worker worker = Worker.CreateWithId(id, name, role);

        // Assert
        Assert.IsNotNull(worker);
        Assert.IsInstanceOfType(worker, typeof(Worker));
        Assert.AreEqual(id, worker.Id);
        Assert.AreEqual(name, worker.Name);
        Assert.AreEqual(role, worker.Role);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Create_InvalidName_ThrowsArgumentException()
    {
        // Arrange
        string invalidName = string.Empty;
        Role role = Role.Employee;

        // Act
        Worker worker = Worker.Create(invalidName, role);

        // Assert
        // Expecting ArgumentException to be thrown
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateWithId_InvalidName_ThrowsArgumentException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string invalidName = null;
        Role role = Role.Manager;

        // Act
        Worker worker = Worker.CreateWithId(id, invalidName, role);

        // Assert
        // Expecting ArgumentException to be thrown
    }
}
