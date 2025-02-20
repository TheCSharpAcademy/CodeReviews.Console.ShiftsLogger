using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.DTOs;

public class WorkerDto
{
    public int WorkerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    public WorkerDto() { }

    public WorkerDto(Worker worker)
    {
        WorkerId = worker.WorkerId;
        FirstName = worker.FirstName;
        LastName = worker.LastName;
        HireDate = worker.HireDate;
        DepartmentId = worker.DepartmentId;
        DepartmentName = worker.Department?.DepartmentName;
    }
}
