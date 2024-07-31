using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Shared;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : Controller<Employee>
{
    public EmployeeController(IService<Employee> service) : base(service)
    {
    }
}