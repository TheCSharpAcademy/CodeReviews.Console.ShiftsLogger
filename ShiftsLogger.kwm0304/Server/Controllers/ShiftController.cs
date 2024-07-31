using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Shared;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : Controller<Shift>
{
    public ShiftController(IService<Shift> service) : base(service)
    {}
}