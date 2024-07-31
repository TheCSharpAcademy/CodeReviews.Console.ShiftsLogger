using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : Controller<Employee>
{
    public EmployeeController(IService<Employee> service) : base(service)
    {
    }
}