using ShiftsLoggerWebApi.Models;

namespace ShiftsLogger;

public class ShiftsLoggerWebApi
{
    public static void Main()
    {
        JsonConfig();
        bool dbInitSuccesful = false;
        try
        {
            dbInitSuccesful = DBInit();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        if (dbInitSuccesful)
            WebApiBuilder();
    }

    public static void WebApiBuilder()
    {
        var builder = WebApplication.CreateBuilder();
        
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddDbContext<ShiftsLoggerContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    public static void JsonConfig()
    {
        try
        {
            IConfiguration jsonConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            ShiftsLoggerContext.ShiftsLoggerConnectionString = jsonConfig.GetConnectionString("DefaultConnection"); //Try catch block
        }
        catch
        {
            Console.WriteLine("Please configure your connection string.");
        }   
    }

    public static bool DBInit()
    {
        var db = new ShiftsLoggerContext();
        db.Database.EnsureCreated();
        return true;
    }

    public static void PopulateDB()
    {
        var db = new ShiftsLoggerContext();
        db.Employees.Add(new Employee{Name = "John Doe", Admin = true});
        db.Employees.Add(new Employee{Name = "John Smith"});
        db.Employees.Add(new Employee{Name = "John Roberts", Admin = true});
        db.Employees.Add(new Employee{Name = "Michael Smith"});
        db.Employees.Add(new Employee{Name = "William Johnson"});
        db.Employees.Add(new Employee{Name = "Mathew Simmons"});
        db.Employees.Add(new Employee{Name = "Donald Jones"});
        db.Employees.Add(new Employee{Name = "John Jones"});
        db.Employees.Add(new Employee{Name = "Paul Anderson"});
        db.Employees.Add(new Employee{Name = "Michael Taylor"});
        db.Employees.Add(new Employee{Name = "Paul Brown"});
        db.Employees.Add(new Employee{Name = "Brian Johnson"});
        db.Employees.Add(new Employee{Name = "Brian Garcia"});
        db.Employees.Add(new Employee{Name = "Megan Brown"});
        db.Employees.Add(new Employee{Name = "Jean Simmons"});
        db.Employees.Add(new Employee{Name = "Judi Smith"});
        db.Employees.Add(new Employee{Name = "Julia Taylor"});
        db.Employees.Add(new Employee{Name = "Sophia Garcia"});
        db.Employees.Add(new Employee{Name = "Marilyn Simmons"});
        db.Employees.Add(new Employee{Name = "Beverly Anderson"});                
        db.SaveChanges();
        
        foreach(Employee employee in db.Employees)
        {
            AddShifts(employee.EmployeeId);
        }
    }

    public static void AddShifts(int id)
    {
        var db = new ShiftsLoggerContext();
        var employee = db.Employees.Where(p => p.EmployeeId == id).First();

        employee.Shifts?.Add(new Shift{ShiftStartTime = new DateTime(2024, 1, 19, 9, 5, 0).ToUniversalTime(), 
            ShiftEndTime = new DateTime(2024, 1, 19, 18, 10, 0).ToUniversalTime()} );
        employee.Shifts?.Add(new Shift{ShiftStartTime = new DateTime(2024, 1, 20, 9, 7, 0).ToUniversalTime(), 
            ShiftEndTime = new DateTime(2024, 1, 20, 18, 2, 0).ToUniversalTime()} );
        employee.Shifts?.Add(new Shift{ShiftStartTime = new DateTime(2024, 1, 21, 9, 3, 0).ToUniversalTime(), 
            ShiftEndTime = new DateTime(2024, 1, 21, 18, 9, 0).ToUniversalTime()} );
        employee.Shifts?.Add(new Shift{ShiftStartTime = new DateTime(2024, 1, 22, 9, 1, 0).ToUniversalTime(), 
            ShiftEndTime = new DateTime(2024, 1, 22, 18, 2, 0).ToUniversalTime()} );
        employee.Shifts?.Add(new Shift{ShiftStartTime = new DateTime(2024, 1, 23, 9, 0, 0).ToUniversalTime(), 
            ShiftEndTime = new DateTime(2024, 1, 23, 18, 0, 0).ToUniversalTime()} );

        db.SaveChanges();
    }
}