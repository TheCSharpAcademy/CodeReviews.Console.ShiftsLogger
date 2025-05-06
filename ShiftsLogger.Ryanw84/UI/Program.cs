using AutoMapper;

using FrontEnd.Data;

using Microsoft.EntityFrameworkCore;

using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Frontend.Data;
using ShiftsLogger.Ryanw84.Services;

using Spectre.Console;

namespace FrontEnd;

public class Program
{
    private DataAccess dataAccess;

    public Program()
    {
        // Initialize DataAccess with proper DbContextOptions
        dataAccess = new DataAccess(new DbContextOptions<ShiftsDbContext>());

        dataAccess.ConfirmConnection();

		var context = new ShiftsDbContext(new DbContextOptions<ShiftsDbContext>());
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		}
}
