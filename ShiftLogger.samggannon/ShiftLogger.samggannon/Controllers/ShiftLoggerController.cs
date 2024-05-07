using Microsoft.AspNetCore.Mvc;
using ShiftLogger.samggannon.Models;

namespace ShiftLogger.samggannon.Controllers;


[ApiController]
[Route("ShiftLog")]
public class ShiftLoggerController : ControllerBase
{
    private readonly ILogger<ShiftLoggerController> _logger;

    public ShiftLoggerController(ILogger<ShiftLoggerController> logger)
    {
        _logger = logger;
    }



};

