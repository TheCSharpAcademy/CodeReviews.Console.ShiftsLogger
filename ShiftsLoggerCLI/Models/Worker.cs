using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShiftsLoggerCLI.Models;

public record WorkerRead(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("position")]
    string Position,
    [property: JsonPropertyName("hireDate")]
    DateTime HireDate)
{
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}, Position: {Position}";
    }
}

public record WorkerCreate(string Name,string Email,string Position,DateTime HireDate);
public  record WorkerUpdate(int Id,string? Name,string? Email,string? Position,DateTime? HireDate);