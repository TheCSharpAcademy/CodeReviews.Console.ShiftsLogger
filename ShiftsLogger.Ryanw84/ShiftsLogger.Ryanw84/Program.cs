using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Mapping;
using ShiftsLogger.Ryanw84.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShiftsLogger.Ryanw84.Frontend.Data;

namespace ShiftsLogger.Ryanw84.FrontEnd;

public class Program
	{
	public static void Main( )
		{
		var context = new ShiftsDbContext(new DbContextOptions<ShiftsDbContext>());
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();

		var dataAccess = new DataAccess(new DbContextOptions<ShiftsDbContext>());

		dataAccess.ConfirmConnection();

		
		}
	}
