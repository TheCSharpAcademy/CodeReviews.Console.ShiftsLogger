    using Microsoft.EntityFrameworkCore;
    using ShiftsLoggerAPI.Data;
    using ShiftsLoggerAPI.Interfaces;
    using ShiftsLoggerAPI.Models;

    namespace ShiftsLoggerAPI.Repository;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ShitftsLoggerDbContext _context;

        public EmployeeRepository(ShitftsLoggerDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var newEmp = await _context.Employees.AddAsync(employee);
            _context.SaveChanges();
            return newEmp.Entity;
        }

        public async Task<ICollection<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<string> DeleteEmployeeAsync(int id)
        {
            var emp = await _context.Employees.AsNoTracking().Where(e => e.EmpId == id).FirstOrDefaultAsync();

            if (emp == null)
                return null;

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return $"Successfully deleted employee {emp.EmpName} with id: {id}";
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee updatedEmployee)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
                return null;

            // Use only the fields that were provided in the request
            if (!string.IsNullOrEmpty(updatedEmployee.EmpName))
                emp.EmpName = updatedEmployee.EmpName;

            if (!string.IsNullOrEmpty(updatedEmployee.EmpPhone))
                emp.EmpPhone = updatedEmployee.EmpPhone;

            await _context.SaveChangesAsync();
            return emp;
        }

        public async Task<Employee> FindEmployeeAsync(int id)
        {
            var emp = await _context.Employees.AsNoTracking().Where(e => e.EmpId == id).FirstOrDefaultAsync();

            if (emp == null)
                return null;

            return emp;
        }

        public async Task<Employee> FindEmployeeAsync(string empName)
        {
            var emp = await _context.Employees.AsNoTracking().Where(e => e.EmpName == empName).FirstOrDefaultAsync();

            if (emp == null)
                return null;

            return emp;
        }
    }