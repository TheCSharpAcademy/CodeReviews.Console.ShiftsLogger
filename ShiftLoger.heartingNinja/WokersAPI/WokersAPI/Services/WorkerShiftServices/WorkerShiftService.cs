namespace WokersAPI.Services.WorkerShiftServices;

public class WorkerShiftService : IWorkerShiftService
{
    private readonly DataContext _context;

    public WorkerShiftService(DataContext context)
    {
        _context = context;
    }
    public async Task<List<WorkerShift>> AddWorker(WorkerShift worker)
    {
        _context.WorkerShift.Add(worker);
        await _context.SaveChangesAsync();

        return await _context.WorkerShift.ToListAsync();
    }

    public async Task<List<WorkerShift>> Delete(int id)
    {
        var workers = await _context.WorkerShift
         .Where(w => w.SuperHeroId == id)
         .ToListAsync();

        if (workers.Count == 0)
            return new List<WorkerShift>();

        _context.WorkerShift.RemoveRange(workers);
        await _context.SaveChangesAsync();

        return await _context.WorkerShift.ToListAsync();
    }

    public async Task<List<WorkerShift>> GetAll()
    {
        return await _context.WorkerShift.ToListAsync();
    }

    public async Task<WorkerShift> GetBySuperHeroId(int id)
    {
        var worker = await _context.WorkerShift
        .Where(w => w.SuperHeroId == id)
        .OrderByDescending(w => w.Id)
        .FirstOrDefaultAsync();

        if (worker == null)
            return null;

        return worker;
    }

    public async Task<List<WorkerShift>> UpdateWorker(WorkerShift request)
    {
        var dbWorker = await _context.WorkerShift.FindAsync(request.Id);
        if (dbWorker == null)
            return null;

        dbWorker.Name = request.Name;
        dbWorker.LoginTime = request.LoginTime;
        dbWorker.LogoutTime = request.LogoutTime;

        await _context.SaveChangesAsync();

        return await _context.WorkerShift.ToListAsync();
    }   
}
