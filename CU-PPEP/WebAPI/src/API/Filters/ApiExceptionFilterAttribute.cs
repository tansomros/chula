using CUPPEP.Application.Common.Exceptions;
using CUPPEP.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CUPPEP.API.Filters;

public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(validationException.Errors));
            context.ExceptionHandled = true;
        }
        else if (context.Exception is DomainException domainException)
        {
            context.Result = new BadRequestObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Domain rule violation",
                Detail = domainException.Message
            });
            context.ExceptionHandled = true;
        }
        else if (context.Exception is NotFoundException notFoundException)
        {
            context.Result = new NotFoundObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource not found",
                Detail = notFoundException.Message
            });
            context.ExceptionHandled = true;
        }
        else if (context.Exception is DbUpdateConcurrencyException)
        {
            context.Result = new ConflictObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Concurrency conflict detected",
                Detail = "ข้อมูลถูกแก้ไขโดยผู้ใช้อื่นในเวลาเดียวกัน กรุณาโหลดข้อมูลล่าสุดแล้วลองใหม่อีกครั้ง"
            });
            context.ExceptionHandled = true;
        }

        base.OnException(context);
    }
}
