namespace Buutyful.ShiftsLogger.Domain;

public class Worker
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Role Role { get; private set; }

    private Worker() { }
    public static Worker Create(string name, Role role) =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = string.IsNullOrWhiteSpace(name) ? 
            throw new ArgumentException("name is empty") : name,
            Role = role
        };
    public static Worker CreateWithId(Guid id, string name, Role role) =>
        new()
        {
            Id = id,
            Name = string.IsNullOrWhiteSpace(name) ?
            throw new ArgumentException("name is empty") : name,
            Role = role
        };
}
public enum Role
{
    None = 0,
    Employee = 1,
    Manager = 2,
}
