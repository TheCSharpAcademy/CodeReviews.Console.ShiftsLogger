using System.Text.Json.Serialization;

namespace Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;

public record WorkerResponse(Guid Id, string Name, Role Role)
{
    public static implicit operator WorkerResponse(Worker worker) => 
        new(worker.Id, worker.Name, worker.Role);
}
public class WorkerResponseJson
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("role")]
    public Role Role { get; set; }
    public override string ToString()
    {
        return $"Name: {Name},\n Role: {Role.ToString()}\n";
    }
}