using Cirtuo.RetailProcurementSystem.Application.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cirtuo.RetailProcurementSystem.Api.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }
        
        var problemDetails = new ProblemDetails
        {
            Title = "Not Found",
            Detail = notFoundException.Message,
            Status = StatusCodes.Status404NotFound,
        };
        
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}