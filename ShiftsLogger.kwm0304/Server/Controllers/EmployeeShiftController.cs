using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeShiftController(IEmployeeShiftService service) : ControllerBase
{
    private readonly IEmployeeShiftService _service = service;
}
