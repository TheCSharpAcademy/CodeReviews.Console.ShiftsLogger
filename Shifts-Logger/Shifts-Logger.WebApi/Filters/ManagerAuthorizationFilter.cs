using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShiftsLogger.WebApi.Filters;

public class ManagerAuthorizationFilter : IActionFilter
{
    private readonly string[] _managerCodes;

    public ManagerAuthorizationFilter(IConfiguration configuration)
    {
        _managerCodes = new string[]
        {
            configuration["ManagerCode1"] ?? string.Empty,
            configuration["ManagerCode2"] ?? string.Empty
        };
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        HttpRequest request = context.HttpContext.Request;
        if (!request.Headers.TryGetValue("X-Manager-Code", out var providedCode) ||
                    String.IsNullOrEmpty(providedCode) ||
                    !_managerCodes.Any(s => s.Equals(providedCode)))
        {
            context.Result = new UnauthorizedResult();  // 401 Unauthorized Response
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
