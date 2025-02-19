using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;
using ShiftLoggerApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ShiftLoggerDatabase");

builder.Services.AddDbContext<ShiftLoggerContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();


app.MapGet("/shifts", async (ShiftLoggerContext db) =>
    await db.Shifts.ToListAsync());

app.MapGet("/shifts/{worker_id}", async (int worker_id, ShiftLoggerContext db) =>
{
    var shifts = await db.Shifts
        .Where(s => s.WorkerId == worker_id)
        .ToListAsync();

    return shifts.Any() ? Results.Ok(shifts) : Results.NotFound();
});

app.MapGet("/shifts/department/{department_id}", async (int department_id, ShiftLoggerContext db) =>
{
    var shifts = await db.Shifts
        .Where(s => db.Workers.Any(w => w.WorkerId == s.WorkerId && w.DepartmentId == department_id))
        .ToListAsync();

    return shifts.Any() ? Results.Ok(shifts) : Results.NotFound();
});

app.MapGet("/departments", async (ShiftLoggerContext db) =>
    await db.Departments.ToListAsync());

app.MapGet("/departments/{department_id}", async (int department_id, ShiftLoggerContext db) =>
{
    var department = await db.Departments.FindAsync(department_id);
    return department is not null ? Results.Ok(department) : Results.NotFound();
});

app.MapGet("/workers", async (ShiftLoggerContext db) =>
    await db.Workers.ToListAsync());

app.MapGet("/workers/{worker_id}", async (int worker_id, ShiftLoggerContext db) =>
{
    var worker = await db.Workers
        .Include(w => w.Department) // Include department details if needed
        .FirstOrDefaultAsync(w => w.WorkerId == worker_id);

    return worker is not null ? Results.Ok(worker) : Results.NotFound();
});

app.MapPost("/shifts", async (Shift shift, ShiftLoggerContext db) =>
{
    db.Shifts.Add(shift);
    await db.SaveChangesAsync();

    return Results.Created($"/shifts/{shift.ShiftId}", shift);
});

app.MapPost("/departments", async (Department department, ShiftLoggerContext db) =>
{
    db.Departments.Add(department);
    await db.SaveChangesAsync();

    return Results.Created($"/departments/{department.DepartmentId}", department);
});

app.MapPost("/workers", async (Worker worker, ShiftLoggerContext db) =>
{
    var departmentExists = await db.Departments.AnyAsync(d => d.DepartmentId == worker.DepartmentId);
    if (!departmentExists)
    {
        return Results.BadRequest("Invalid DepartmentId. Department does not exist.");
    }

    db.Workers.Add(worker);
    await db.SaveChangesAsync();

    return Results.Created($"/workers/{worker.WorkerId}", worker);
});

app.MapPut("/workers/{worker_id}", async (int worker_id, Worker updatedWorker, ShiftLoggerContext db) =>
{
    var worker = await db.Workers.FindAsync(worker_id);
    if (worker is null)
    {
        return Results.NotFound();
    }

    var departmentExists = await db.Departments.AnyAsync(d => d.DepartmentId == updatedWorker.DepartmentId);
    if (!departmentExists)
    {
        return Results.BadRequest("Invalid DepartmentId. Department does not exist.");
    }

    worker.FirstName = updatedWorker.FirstName;
    worker.LastName = updatedWorker.LastName;
    worker.HireDate = updatedWorker.HireDate;
    worker.DepartmentId = updatedWorker.DepartmentId;

    await db.SaveChangesAsync();
    return Results.Ok(worker);
});

app.MapPut("/shifts/{shift_id}", async (int shift_id, Shift updatedShift, ShiftLoggerContext db) =>
{
    var shift = await db.Shifts.FindAsync(shift_id);
    if (shift is null)
    {
        return Results.NotFound();
    }

    var workerExists = await db.Workers.AnyAsync(w => w.WorkerId == updatedShift.WorkerId);
    if (!workerExists)
    {
        return Results.BadRequest("Invalid WorkerId. Worker does not exist.");
    }

    if (updatedShift.StartDate >= updatedShift.EndDate)
    {
        return Results.BadRequest("StartDate must be before EndDate.");
    }

    shift.StartDate = updatedShift.StartDate;
    shift.EndDate = updatedShift.EndDate;
    shift.WorkerId = updatedShift.WorkerId;

    await db.SaveChangesAsync();
    return Results.Ok(shift);
});

app.MapDelete("/shifts/{shift_id}", async (int shift_id, ShiftLoggerContext db) =>
{
    var shift = await db.Shifts.FindAsync(shift_id);
    if (shift is null)
    {
        return Results.NotFound();
    }

    db.Shifts.Remove(shift);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/workers/{worker_id}", async (int worker_id, ShiftLoggerContext db) =>
{
    var worker = await db.Workers.FindAsync(worker_id);
    if (worker is null)
    {
        return Results.NotFound();
    }

    bool hasShifts = await db.Shifts.AnyAsync(s => s.WorkerId == worker_id);
    if (hasShifts)
    {
        return Results.BadRequest("Cannot delete worker. There are associated shifts.");
    }

    db.Workers.Remove(worker);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/departments/{department_id}", async (int department_id, ShiftLoggerContext db) =>
{
    var department = await db.Departments.FindAsync(department_id);
    if (department is null)
    {
        return Results.NotFound();
    }

    bool hasWorkers = await db.Workers.AnyAsync(w => w.DepartmentId == department_id);
    if (hasWorkers)
    {
        return Results.BadRequest("Cannot delete department. There are associated workers.");
    }

    db.Departments.Remove(department);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
