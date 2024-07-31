using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Dtos;
using Server.Services.Interfaces;
using Spectre.Console;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController : Controller<Shift>
{
    public ShiftController(IService<Shift> service) : base(service)
    {}
}