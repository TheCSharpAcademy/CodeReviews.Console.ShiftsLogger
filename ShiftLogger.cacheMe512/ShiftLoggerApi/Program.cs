using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;
using ShiftLoggerApi.DTOs;
using ShiftLoggerApi;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShiftLoggerDatabase");

builder.Services.AddDbContext<ShiftLoggerContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

var shifts = app.MapGroup("/shifts");
var departments = app.MapGroup("/departments");
var workers = app.MapGroup("/workers");

shifts.MapGet("/", async (int? worker_id, ShiftLoggerContext db) =>
{
    if (worker_id.HasValue)
    {
        return await GetShiftsByWorker(worker_id, db);
    }
    return await GetAllShifts(db);
});

shifts.MapGet("/{shift_id}", GetShift);
shifts.MapGet("/department/{department_id}", GetShiftsByDepartment);
shifts.MapPost("/", CreateShift);
shifts.MapPut("/{shift_id}", UpdateShift);
shifts.MapDelete("/{shift_id}", DeleteShift);

departments.MapGet("/", GetAllDepartments);
departments.MapGet("/{department_id}", GetDepartment);
departments.MapPost("/", CreateDepartment);
departments.MapPut("/{department_id}", UpdateDepartment);
departments.MapDelete("/{department_id}", DeleteDepartment);

workers.MapGet("/", GetAllWorkers);
workers.MapGet("/{worker_id}", GetWorker);
workers.MapPost("/", CreateWorker);
workers.MapPut("/{worker_id}", UpdateWorker);
workers.MapDelete("/{worker_id}", DeleteWorker);

app.Run();


static async Task<IResult> GetAllWorkers(ShiftLoggerContext db)
{
    return TypedResults.Ok(await db.Workers.Include(w => w.Department).Select(w => new WorkerDto(w)).ToListAsync());
}

static async Task<IResult> GetWorker(int worker_id, ShiftLoggerContext db)
{
    var worker = await db.Workers.Include(w => w.Department).FirstOrDefaultAsync(w => w.WorkerId == worker_id);
    return worker is not null ? TypedResults.Ok(new WorkerDto(worker)) : TypedResults.NotFound();
}

static async Task<IResult> CreateWorker(WorkerDto workerDTO, ShiftLoggerContext db)
{
    if (!await db.Departments.AnyAsync(d => d.DepartmentId == workerDTO.DepartmentId))
    {
        return TypedResults.BadRequest("Invalid DepartmentId. Department does not exist.");
    }

    var worker = new Worker
    {
        FirstName = workerDTO.FirstName,
        LastName = workerDTO.LastName,
        HireDate = workerDTO.HireDate,
        DepartmentId = workerDTO.DepartmentId
    };

    db.Workers.Add(worker);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/workers/{worker.WorkerId}", new WorkerDto(worker));
}

static async Task<IResult> UpdateWorker(int worker_id, WorkerDto updatedWorkerDto, ShiftLoggerContext db)
{
    var worker = await db.Workers.FindAsync(worker_id);
    if (worker is null) return TypedResults.NotFound();

    worker.FirstName = updatedWorkerDto.FirstName;
    worker.LastName = updatedWorkerDto.LastName;
    worker.HireDate = updatedWorkerDto.HireDate;
    worker.DepartmentId = updatedWorkerDto.DepartmentId;

    await db.SaveChangesAsync();
    return TypedResults.Ok(new WorkerDto(worker));
}

static async Task<IResult> DeleteWorker(int worker_id, ShiftLoggerContext db)
{
    var worker = await db.Workers.FindAsync(worker_id);
    if (worker is null) return TypedResults.NotFound();
    db.Workers.Remove(worker);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> GetAllShifts(ShiftLoggerContext db)
{
    return TypedResults.Ok(await db.Shifts.Select(s => new ShiftDto(s)).ToListAsync());
}

static async Task<IResult> GetShift(int shift_id, ShiftLoggerContext db)
{
    var shift = await db.Shifts.Include(s => s.Worker).FirstOrDefaultAsync(s => s.ShiftId == shift_id);
    return shift is not null ? TypedResults.Ok(new ShiftDto(shift)) : TypedResults.NotFound();
}

static async Task<IResult> GetShiftsByWorker(int? worker_id, ShiftLoggerContext db)
{
    var shifts = await db.Shifts.Where(s => s.WorkerId == worker_id).Select(s => new ShiftDto(s)).ToListAsync();
    return shifts.Any() ? TypedResults.Ok(shifts) : TypedResults.NotFound();
}

static async Task<IResult> GetShiftsByDepartment(int department_id, ShiftLoggerContext db)
{
    var shifts = await db.Shifts
        .Where(s => db.Workers.Any(w => w.WorkerId == s.WorkerId && w.DepartmentId == department_id))
        .Select(s => new ShiftDto(s))
        .ToListAsync();

    return shifts.Any() ? TypedResults.Ok(shifts) : TypedResults.NotFound();
}

static async Task<IResult> CreateShift(ShiftDto shiftDTO, ShiftLoggerContext db)
{
    var shift = new Shift { StartDate = shiftDTO.StartDate, EndDate = shiftDTO.EndDate, WorkerId = shiftDTO.WorkerId };
    db.Shifts.Add(shift);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/shifts/{shift.ShiftId}", new ShiftDto(shift));
}

static async Task<IResult> UpdateShift(int shift_id, ShiftDto updatedShiftDto, ShiftLoggerContext db)
{
    var shift = await db.Shifts.FindAsync(shift_id);
    if (shift is null) return TypedResults.NotFound();

    if (!await db.Workers.AnyAsync(w => w.WorkerId == updatedShiftDto.WorkerId))
    {
        return TypedResults.BadRequest("Invalid WorkerId. Worker does not exist.");
    }

    if (updatedShiftDto.StartDate >= updatedShiftDto.EndDate)
    {
        return TypedResults.BadRequest("StartDate must be before EndDate.");
    }

    shift.StartDate = updatedShiftDto.StartDate;
    shift.EndDate = updatedShiftDto.EndDate;
    shift.WorkerId = updatedShiftDto.WorkerId;

    await db.SaveChangesAsync();
    return TypedResults.Ok(new ShiftDto(shift));
}

static async Task<IResult> DeleteShift(int shift_id, ShiftLoggerContext db)
{
    var shift = await db.Shifts.FindAsync(shift_id);
    if (shift is null) return TypedResults.NotFound();
    db.Shifts.Remove(shift);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> GetAllDepartments(ShiftLoggerContext db)
{
    return TypedResults.Ok(await db.Departments.Select(d => new DepartmentDto(d)).ToListAsync());
}

static async Task<IResult> GetDepartment(int department_id, ShiftLoggerContext db)
{
    var department = await db.Departments.FindAsync(department_id);
    return department is not null ? TypedResults.Ok(new DepartmentDto(department)) : TypedResults.NotFound();
}

static async Task<IResult> CreateDepartment(DepartmentDto departmentDTO, ShiftLoggerContext db)
{
    var department = new Department { DepartmentName = departmentDTO.DepartmentName };
    db.Departments.Add(department);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/departments/{department.DepartmentId}", new DepartmentDto(department));
}

static async Task<IResult> UpdateDepartment(int department_id, DepartmentDto updatedDepartmentDto, ShiftLoggerContext db)
{
    var department = await db.Departments.FindAsync(department_id);
    if (department is null) return TypedResults.NotFound();

    department.DepartmentName = updatedDepartmentDto.DepartmentName;
    await db.SaveChangesAsync();
    return TypedResults.Ok(new DepartmentDto(department));
}

static async Task<IResult> DeleteDepartment(int department_id, ShiftLoggerContext db)
{
    var department = await db.Departments.FindAsync(department_id);
    if (department is null) return TypedResults.NotFound();
    db.Departments.Remove(department);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}
