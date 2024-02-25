namespace Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;

public record WorkerResponse(Guid Id, string Name, Role Role)
{
    public static implicit operator WorkerResponse(Worker worker) => 
        new(worker.Id, worker.Name, worker.Role);
}