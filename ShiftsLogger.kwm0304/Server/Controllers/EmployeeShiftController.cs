using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeShiftController : Controller<EmployeeShift>
{
    public EmployeeShiftController(IService<EmployeeShift> service) : base(service)
    {
    }

}
