using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Learn.Blazor.Net6.Pag.Server.Models;

namespace Learn.Blazor.Net6.Pag.Server.Infrastructures.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            OperationCanceledException => new StatusCodeResult(StatusCodes.Status409Conflict),
            _ => new JsonResult(new GlobalExceptionModel
            {
                Type = context.Exception.GetType().ToString(),
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = "application/json"
            }
        };
    }
}
