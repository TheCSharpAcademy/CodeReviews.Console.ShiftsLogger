using Microsoft.AspNetCore.Mvc;

namespace WokersAPI.Services.WorkerShiftServices
{
    public interface IWorkerShiftService
    {
        Task<List<WorkerShift>> GetAll();
        Task<WorkerShift> GetBySuperHeroId(int id);
        Task<List<WorkerShift>> AddWorker(WorkerShift worker);
        Task<List<WorkerShift>> UpdateWorker(WorkerShift request);
        Task<List<WorkerShift>> Delete(int id);
    }
}
