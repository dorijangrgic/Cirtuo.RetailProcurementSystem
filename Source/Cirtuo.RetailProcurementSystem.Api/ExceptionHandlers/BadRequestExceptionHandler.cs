using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cirtuo.RetailProcurementSystem.Api.ExceptionHandlers;

public class BadRequestExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ApplicationException applicationException)
        {
            return false;
        }
        
        var problemDetails = new ProblemDetails
        {
            Title = "Bad request",
            Detail = applicationException.Message,
            Status = StatusCodes.Status400BadRequest,
        };
        
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}