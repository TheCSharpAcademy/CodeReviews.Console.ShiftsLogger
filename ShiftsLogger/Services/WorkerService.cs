using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services
{
    public interface IWorkerService
    {
        Task AddWorkerAsync(WorkerCreate worker);
        Task<bool> RemoveWorkerAsync(int workerId);
        Task<bool> UpdateWorkerAsync(WorkerUpdate newWorker);
        Task<List<Worker>> GetAllWorkersAsync();
        Task<Worker> GetWorkerAsync(int id);
    }
    public class WorkerService : IWorkerService
    {
        private readonly ShiftsLoggerContext _ctx;

        // Inject DbContext through Dependency Injection
        public WorkerService(ShiftsLoggerContext context)
        {
            _ctx = context;
        }
        public async Task AddWorkerAsync(WorkerCreate worker)
        {
            
                _ctx.Workers.Add(new Worker(worker));
                await _ctx.SaveChangesAsync();
                Console.WriteLine("Added Worker");
                await Task.CompletedTask;
            
        }

        public async Task<List<Worker>> GetAllWorkersAsync()
        {
           
                return await _ctx.Workers.ToListAsync();
            
        }

        public async Task<Worker> GetWorkerAsync(int id)
        {
           
                return await _ctx.Workers.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<bool> RemoveWorkerAsync(int workerId)
        {
             var workerToDelete = await _ctx.Workers.FindAsync(workerId);
                if (workerToDelete is null)
                    return false;
                _ctx.Workers.Remove(workerToDelete);
                await _ctx.SaveChangesAsync();

                return true;

            
        }

        public async Task<bool> UpdateWorkerAsync(WorkerUpdate newWorker)
        {
            using (ShiftsLoggerContext ctx = new())
            {
                var worker = await _ctx.Workers.FindAsync(newWorker.Id);
                if (worker is null)
                    return false;
                if (!string.IsNullOrEmpty(newWorker.Name))
                    worker.Name = newWorker.Name;
                if (!string.IsNullOrEmpty(newWorker.Email))
                    worker.Email = newWorker.Email;
                if (!string.IsNullOrEmpty(newWorker.Position))
                    worker.Position = newWorker.Position;
                if (newWorker.HireDate is not null && !newWorker.HireDate.Equals(DateTime.MinValue) && newWorker.HireDate < DateTime.Now)
                    worker.HireDate = (DateTime)newWorker.HireDate;

                await _ctx.SaveChangesAsync();
                return true;
            }
        }

       
    }
}
