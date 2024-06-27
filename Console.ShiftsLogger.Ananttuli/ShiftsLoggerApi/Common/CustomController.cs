using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Controllers;

public class CustomController : ControllerBase
{
    public ActionResult ErrorResponse(Error? error)
    {
        return error?.Type switch
        {
            ErrorType.BusinessRuleValidation => BadRequest(error),
            ErrorType.DatabaseNotFound => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError, error)
        };
    }
}