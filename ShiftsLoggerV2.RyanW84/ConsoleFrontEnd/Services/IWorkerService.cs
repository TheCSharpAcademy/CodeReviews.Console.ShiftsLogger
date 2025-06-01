using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

namespace ConsoleFrontEnd.Services;
public interface IWorkerService
{
	public Task<ApiResponseDto<List<Workers>>> GetAllWorkers(WorkerFilterOptions workerOptions);
	public Task<ApiResponseDto<List<Workers?>>> GetWorkerById(int id);
	public Task<ApiResponseDto<Workers>> CreateWorker(Workers worker);
	public Task<ApiResponseDto<Workers?>> UpdateWorker(int id , Workers updatedWorker);
	public Task<ApiResponseDto<string?>> DeleteWorker(int id);
}
