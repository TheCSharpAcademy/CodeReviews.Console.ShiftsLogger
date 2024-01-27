using ShiftsLoggerWebApi.Models;

namespace ShiftsLogger;

public class ShiftsLoggerWebAPI
{
    public static void Main()
    {
        JsonConfig();
        DBInit();
        PopulateDB();
        WebAPIBuilder();
    }

    public static void WebAPIBuilder()
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

        // app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    public static void JsonConfig()
    {
        IConfiguration jsonConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        ShiftsLoggerContext.ShiftsLoggerConnectionString = jsonConfig.GetConnectionString("DefaultConnection");
    }

    public static void DBInit()
    {
        var db = new ShiftsLoggerContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    public static void PopulateDB()
    {
        var db = new ShiftsLoggerContext();
        db.Employees.Add(new Employee{Name = "Employee1", Admin = true});
        db.Employees.Add(new Employee{Name = "Employee2"});
        db.Employees.Add(new Employee{Name = "Employee3", Admin = true});
        db.Employees.Add(new Employee{Name = "Employee4"});
        db.Employees.Add(new Employee{Name = "Employee5"});
        db.Employees.Add(new Employee{Name = "Employee6"});
        db.Employees.Add(new Employee{Name = "Employee7"});
        db.Employees.Add(new Employee{Name = "Employee8"});
        db.Employees.Add(new Employee{Name = "Employee9"});
        db.Employees.Add(new Employee{Name = "Employee10"});
        db.Employees.Add(new Employee{Name = "Employee11"});
        db.Employees.Add(new Employee{Name = "Employee12"});
        db.Employees.Add(new Employee{Name = "Employee13"});
        db.Employees.Add(new Employee{Name = "Employee14"});
        db.Employees.Add(new Employee{Name = "Employee15"});
        db.Employees.Add(new Employee{Name = "Employee16"});
        db.Employees.Add(new Employee{Name = "Employee17"});
        db.Employees.Add(new Employee{Name = "Employee18"});
        db.Employees.Add(new Employee{Name = "Employee19"});
        db.Employees.Add(new Employee{Name = "Employee20"});                
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