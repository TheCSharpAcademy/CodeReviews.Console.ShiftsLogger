using System.Text.Json.Serialization;

namespace Models;

public class PostWorkerDto {

    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }
    
    [JsonPropertyName("position")]
    public required string Position { get; set; }
}

public class PutWorkerDto {

    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("position")]
    public required string Position { get; set; }
}

public class GetWorkerDto {

    [JsonPropertyName("firstName")] 
    public required string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("position")]
    public required string Position { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }
}