using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Diagnostics;

namespace ShiftLogger;

public static class Program
{
    
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<ShiftDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging()
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole();
        });

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ShiftDbContext>();
            db.Database.EnsureCreated();
        }

        app.MapPost("/shifts", async (ShiftDbContext db, Shift shift) =>
        {
            db.Shift.Add(shift);
            await db.SaveChangesAsync();
            return Results.Created($"/shifts/{shift.Id}", shift);
        });

        app.MapGet("/shifts", async (ShiftDbContext db) =>
        {
            return Results.Ok(await db.Shift.ToListAsync());
        });

        app.MapGet("/shifts/id/{id}", async (ShiftDbContext db, int id) =>
        {
            var shift = await db.Shift.FindAsync(id);
            if (shift == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(shift);
        });

        app.MapGet("/shifts/name/{name}", async (ShiftDbContext db, string name) =>
        {
            var shifts = await db.Shift
                .Where(s => s.Employee.ToUpper() == name.ToUpper()).ToListAsync();

            if (shifts == null || shifts.Count == 0)
            {
                return Results.NotFound();
            }
            return Results.Ok(shifts);
        });

        app.MapPut("/shifts/id/{id}", async (ShiftDbContext db, int id, Shift shift) =>
        {
            if (id != shift.Id)
            {
                return Results.BadRequest();
            }
            db.Entry(shift).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Results.Ok(shift);
        });

        app.MapDelete("/shifts/id/{id}", async (ShiftDbContext db, int id) =>
        {
            var shift = await db.Shift.FindAsync(id);
            if (shift == null)
            {
                return Results.NotFound();
            }
            db.Shift.Remove(shift);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}
