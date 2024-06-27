using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Common;

public class CustomController : ControllerBase
{
    [NonAction]
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