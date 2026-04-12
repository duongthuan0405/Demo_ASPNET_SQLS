
using webapi.Application.BusinessExceptions;
using webapi.WebAPI.DTOs;

namespace webapi.WebAPI.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await Task.Delay(1000);
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                if(ex is BusinessException businessEx)
                {
                    if(ex is InvalidUseCasesInputException invalidInputEx)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new ErrorResponse
                        {
                            Message = invalidInputEx.Message,
                            Errors = invalidInputEx.GetErrors(),
                            StatusCode = 400
                        };
                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }

                    else if(ex is UnauthorizedException unauthorizedEx)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new ErrorResponse
                        {
                            Message = ex.Message,
                            Errors = unauthorizedEx.GetErrors(),
                            StatusCode = 401
                        };
                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }
                    else if (ex is ConflictUniqueValueException conflictUniqueValueEx)
                    {
                        context.Response.StatusCode = 429;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new ErrorResponse
                        {
                            Message = conflictUniqueValueEx.Message,
                            Errors = conflictUniqueValueEx.GetErrors(),
                            StatusCode = 429
                        };
                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        var errorResponse = new ErrorResponse
                        {
                            Message = "An unexpected error occurred.",
                            Errors = businessEx.GetErrors(),
                            StatusCode = 500
                        };
                        await context.Response.WriteAsJsonAsync(errorResponse);
                    }
                }
                else
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var errorResponse = new ErrorResponse
                    {
                        Message = "An unexpected error occurred.",
                        Errors = null,
                        StatusCode = 500
                    };
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }
        }
    }
}
