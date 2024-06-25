using SharedLibrary.Models;

namespace ShiftsLoggerAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
