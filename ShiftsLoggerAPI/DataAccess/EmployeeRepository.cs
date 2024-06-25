using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.DataAccess
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private ShiftLoggerContext _context;

        public EmployeeRepository(ShiftLoggerContext context)
        {
            _context = context;
        }

        public void Create(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _context.Remove(employee);
            _context.SaveChanges();
        }

        public Employee GetById(int id)
        {
            return _context.Employees
                .Include(e => e.Shifts)
                .FirstOrDefault(e => e.Id == id)!;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees
                .Include(e => e.Shifts)
                .ToList();
        }

        public void Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.Update(employee);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }


    }
}
