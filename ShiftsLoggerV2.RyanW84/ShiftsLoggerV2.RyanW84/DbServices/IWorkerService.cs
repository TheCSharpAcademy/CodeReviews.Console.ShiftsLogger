using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

namespace ShiftsLoggerV2.RyanW84.Services;

public interface IWorkerService
{
    public Task<ApiResponseDto<List<Workers>>> GetAllWorkers(WorkerFilterOptions workerOptions);
    public Task<ApiResponseDto<Workers?>> GetWorkerById(int id);
    public Task<ApiResponseDto<Workers>> CreateWorker(WorkerApiRequestDto worker);
    public Task<ApiResponseDto<Workers?>> UpdateWorker(int id, WorkerApiRequestDto updatedWorker);
    public Task<ApiResponseDto<string?>> DeleteWorker(int id);
}
