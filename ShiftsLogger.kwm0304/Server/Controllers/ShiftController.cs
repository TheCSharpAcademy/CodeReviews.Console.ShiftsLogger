using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Shared;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : Controller<Shift>
{
    public ShiftsController(IService<Shift> service) : base(service)
    {}
}