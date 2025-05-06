namespace FrontEnd.Data;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShiftsLogger.Ryanw84.Data;

public class DataAccess : ShiftsDbContext
{
    private IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    private readonly string ConnectionString;

    public DataAccess(DbContextOptions<ShiftsDbContext> options)
        : base(options)
    {
        ConnectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' is not configured."
            );
    }

    public bool ConfirmConnection()
    {
        try
        {
            Console.WriteLine("*_*_*_* Flashcards *_*_*_* ");
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.Write(
                    $"\nConnection Status: {System.Data.ConnectionState.Open}\nPress any Key to continue:"
                );
                Console.ReadKey();
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
