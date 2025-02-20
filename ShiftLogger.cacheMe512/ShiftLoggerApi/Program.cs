using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;
using ShiftLoggerApi;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShiftLoggerDatabase");

builder.Services.AddDbContext<ShiftLoggerContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

var shifts = app.MapGroup("/shifts");
var departments = app.MapGroup("/departments");
var workers = app.MapGroup("/workers");

shifts.MapGet("/", GetAllShifts);
shifts.MapGet("/{worker_id}", GetShiftsByWorker);
shifts.MapGet("/department/{department_id}", GetShiftsByDepartment);
shifts.MapPost("/", CreateShift);
shifts.MapPut("/{shift_id}", UpdateShift);
shifts.MapDelete("/{shift_id}", DeleteShift);

departments.MapGet("/", GetAllDepartments);
departments.MapGet("/{department_id}", GetDepartment);
departments.MapPost("/", CreateDepartment);
departments.MapDelete("/{department_id}", DeleteDepartment);

workers.MapGet("/", GetAllWorkers);
workers.MapGet("/{worker_id}", GetWorker);
workers.MapPost("/", CreateWorker);
workers.MapPut("/{worker_id}", UpdateWorker);
workers.MapDelete("/{worker_id}", DeleteWorker);

app.Run();

static async Task<IResult> GetAllShifts(ShiftLoggerContext db)
{
    return TypedResults.Ok(await db.Shifts.ToListAsync());
}

static async Task<IResult> GetShiftsByWorker(int worker_id, ShiftLoggerContext db)
{
    var shifts = await db.Shifts.Where(s => s.WorkerId == worker_id).ToListAsync();
    return shifts.Any() ? TypedResults.Ok(shifts) : TypedResults.NotFound();
}

static async Task<IResult> GetShiftsByDepartment(int department_id, ShiftLoggerContext db)
{
    var shifts = await db.Shifts
        .Where(s => db.Workers.Any(w => w.WorkerId == s.WorkerId && w.DepartmentId == department_id))
        .ToListAsync();
    return shifts.Any() ? TypedResults.Ok(shifts) : TypedResults.NotFound();
}

static async Task<IResult> GetAllDepartments(ShiftLoggerContext db)
{
    return TypedResults.Ok(await db.Departments.ToListAsync());
}

static async Task<IResult> GetDepartment(int department_id, ShiftLoggerContext db)
{
    var department = await db.Departments.FindAsync(department_id);
    return department is not null ? TypedResults.Ok(department) : TypedResults.NotFound();
}

static async Task<IResult> GetAllWorkers(ShiftLoggerContext db)
{
    return TypedResults.Ok(await db.Workers.ToListAsync());
}

static async Task<IResult> GetWorker(int worker_id, ShiftLoggerContext db)
{
    var worker = await db.Workers.Include(w => w.Department).FirstOrDefaultAsync(w => w.WorkerId == worker_id);
    return worker is not null ? TypedResults.Ok(worker) : TypedResults.NotFound();
}

static async Task<IResult> CreateShift(Shift shift, ShiftLoggerContext db)
{
    db.Shifts.Add(shift);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/shifts/{shift.ShiftId}", shift);
}

static async Task<IResult> CreateDepartment(Department department, ShiftLoggerContext db)
{
    db.Departments.Add(department);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/departments/{department.DepartmentId}", department);
}

static async Task<IResult> CreateWorker(Worker worker, ShiftLoggerContext db)
{
    if (!await db.Departments.AnyAsync(d => d.DepartmentId == worker.DepartmentId))
    {
        return TypedResults.BadRequest("Invalid DepartmentId. Department does not exist.");
    }

    db.Workers.Add(worker);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/workers/{worker.WorkerId}", worker);
}

static async Task<IResult> UpdateWorker(int worker_id, Worker updatedWorker, ShiftLoggerContext db)
{
    var worker = await db.Workers.FindAsync(worker_id);
    if (worker is null) return TypedResults.NotFound();

    if (!await db.Departments.AnyAsync(d => d.DepartmentId == updatedWorker.DepartmentId))
    {
        return TypedResults.BadRequest("Invalid DepartmentId. Department does not exist.");
    }

    worker.FirstName = updatedWorker.FirstName;
    worker.LastName = updatedWorker.LastName;
    worker.HireDate = updatedWorker.HireDate;
    worker.DepartmentId = updatedWorker.DepartmentId;

    await db.SaveChangesAsync();
    return TypedResults.Ok(worker);
}

static async Task<IResult> UpdateShift(int shift_id, Shift updatedShift, ShiftLoggerContext db)
{
    var shift = await db.Shifts.FindAsync(shift_id);
    if (shift is null) return TypedResults.NotFound();

    if (!await db.Workers.AnyAsync(w => w.WorkerId == updatedShift.WorkerId))
    {
        return TypedResults.BadRequest("Invalid WorkerId. Worker does not exist.");
    }

    if (updatedShift.StartDate >= updatedShift.EndDate)
    {
        return TypedResults.BadRequest("StartDate must be before EndDate.");
    }

    shift.StartDate = updatedShift.StartDate;
    shift.EndDate = updatedShift.EndDate;
    shift.WorkerId = updatedShift.WorkerId;

    await db.SaveChangesAsync();
    return TypedResults.Ok(shift);
}

static async Task<IResult> DeleteShift(int shift_id, ShiftLoggerContext db)
{
    var shift = await db.Shifts.FindAsync(shift_id);
    if (shift is null) return TypedResults.NotFound();

    db.Shifts.Remove(shift);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteWorker(int worker_id, ShiftLoggerContext db)
{
    var worker = await db.Workers.FindAsync(worker_id);
    if (worker is null) return TypedResults.NotFound();

    if (await db.Shifts.AnyAsync(s => s.WorkerId == worker_id))
    {
        return TypedResults.BadRequest("Cannot delete worker. There are associated shifts.");
    }

    db.Workers.Remove(worker);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteDepartment(int department_id, ShiftLoggerContext db)
{
    var department = await db.Departments.FindAsync(department_id);
    if (department is null) return TypedResults.NotFound();

    if (await db.Workers.AnyAsync(w => w.DepartmentId == department_id))
    {
        return TypedResults.BadRequest("Cannot delete department. There are associated workers.");
    }

    db.Departments.Remove(department);
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}
