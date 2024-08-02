using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Shared;

namespace Server.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : Controller<Employee>
{
    public EmployeesController(IService<Employee> service) : base(service)
    {
    }
}