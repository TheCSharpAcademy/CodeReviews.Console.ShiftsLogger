using HKhemanthSharma.ShiftLoggerAPI.Model;
using HKhemanthSharma.ShiftLoggerAPI.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace HKhemanthSharma.ShiftLoggerAPI.Services
{
    public interface IWorkerRepository
    {
        Task<List<Worker>> GetAllWorkerAsync();
        Task<Worker> GetWorkerAsync(int Id);
        Task<Worker> CreateWorkerAsync(WorkerDto NewWorker);
        Task<Worker> DeleteWorkerAsync(int Id);
        Task<Worker> UpdateWorkerAsync(WorkerDto NewWorker, int Id);
    }
    public class WorkerRepository : IWorkerRepository
    {
        private readonly ShiftDbContext context;
        public WorkerRepository(ShiftDbContext _context)
        {
            context = _context;
        }
        public async Task<List<Worker>> GetAllWorkerAsync()
        {
            return await context.Workers.Include(x => x.Shifts).ToListAsync();

        }
        public async Task<Worker> GetWorkerAsync(int Id)
        {
            return await context.Workers.Include(x => x.Shifts).FirstOrDefaultAsync(x => x.WorkerId == Id);
        }
        public async Task<Worker> CreateWorkerAsync(WorkerDto NewWorker)
        {
            Worker NewDomainWorker = new Worker
            {
                Name = NewWorker.Name
            };
            var AddedWorker = await context.Workers.AddAsync(NewDomainWorker);
            await context.SaveChangesAsync();
            return AddedWorker.Entity;
        }
        public async Task<Worker> UpdateWorkerAsync(WorkerDto NewWorker, int Id)
        {
            Worker UpdateWorker = await context.Workers.Include(x => x.Shifts).FirstOrDefaultAsync(x => x.WorkerId == Id);
            if (UpdateWorker == null)
            {
                return null;
            }
            UpdateWorker.Name = NewWorker.Name;
            context.Workers.Update(UpdateWorker);
            await context.SaveChangesAsync();
            return UpdateWorker;
        }
        public async Task<Worker> DeleteWorkerAsync(int Id)
        {
            Worker DeleteWorker = await context.Workers.FirstOrDefaultAsync(x => x.WorkerId == Id);
            context.Workers.Remove(DeleteWorker);
            await context.SaveChangesAsync();
            return DeleteWorker;
        }
    }
}
