using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.DTOs;

public class DepartmentDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }

    public DepartmentDto() { }

    public DepartmentDto(Department department)
    {
        DepartmentId = department.DepartmentId;
        DepartmentName = department.DepartmentName;
    }
}
