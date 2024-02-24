namespace Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;

public record UpsertWorkerRequest(Guid Id, string Name, Role Role);

