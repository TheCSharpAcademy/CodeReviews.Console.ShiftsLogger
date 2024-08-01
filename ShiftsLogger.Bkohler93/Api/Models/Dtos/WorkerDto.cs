namespace Api.Models.Dtos;

# pragma warning disable CS1591
public class PostWorkerDto {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Position { get; set; }
}

public class PutWorkerDto {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Position { get; set; }
}

public class GetWorkerDto {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Position { get; set; }
    public int Id { get; set; }
}
# pragma warning restore CS1561