using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftApi.DTOs;
using ShiftApi.Models;

namespace ShiftApi.Service;

public class EmployeeService
{
    private readonly ShiftApiContext _context;

    public EmployeeService(ShiftApiContext context)
    {
        _context = context;
    }

    internal async Task<List<EmployeeDTO>> GetAllEmployees()
    {
        var employees = await _context.Employees
            .Include(e => e.Shifts)
            .ToListAsync();

        var employeesDTO = new List<EmployeeDTO>();

        foreach (var e in employees)
        {
            var employeeDTO = new EmployeeDTO
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Shifts = e.Shifts.Select(s => new ShiftDTO
                {
                    ShiftId = s.ShiftId,
                    ShiftStartTime = s.ShiftStartTime,
                    ShiftEndTime = s.ShiftEndTime
                }).ToList<ShiftDTO>()
            };

            employeesDTO.Add(employeeDTO);
        }
        return employeesDTO;
    }

    internal async Task<EmployeeDTO?> GetEmployeeById(int id)
    {
        var employee = await _context.Employees
            .Include (e => e.Shifts)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);

        var employeeDTO = new EmployeeDTO
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Shifts = employee.Shifts.Select(s => new ShiftDTO
            {
                ShiftId = s.ShiftId,
                ShiftStartTime = s.ShiftStartTime,
                ShiftEndTime = s.ShiftEndTime
            }).ToList()
        };

        return employeeDTO;
    }

    internal async Task PostEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    internal async Task UpdateEmployee(int id, EmployeeUpdateDTO employeeDTO)
    {
        var employee = await _context.Employees.FindAsync(id);

        employee.FirstName = employeeDTO.FirstName;
        employee.LastName = employeeDTO.LastName;

        _context.Employees.Update(employee);

        await _context.SaveChangesAsync();
    }

    internal async Task Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    internal bool EmployeeExists(Employee employee)
    {
        return _context.Employees.Any(e => e.EmployeeId == employee.EmployeeId);
    }

    internal async Task<Employee?> FindAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        return employee;
    }
}
