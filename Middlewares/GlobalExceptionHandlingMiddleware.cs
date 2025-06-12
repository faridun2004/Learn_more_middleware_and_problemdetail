
using FluentValidation;
using RegisterService.Exceptions;
using System.Text.Json;

namespace RegisterService.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.ContentType = "application/problem+json";

                if (ex is ValidationException validationException)
                {
                    var problem = new AppProblemDetails(
                        title: "Validation error",
                        detail: "One or more validation errors occurred",
                        statusCode: StatusCodes.Status400BadRequest,
                        errorCode: "VALIDATION_ERROR"
                    );

                    problem.Extensions["errors"] = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        );

                    context.Response.StatusCode = problem.Status ?? StatusCodes.Status400BadRequest;
                    await WriteProblemDetails(context, problem);
                }
                else if (ex is AppException appException)
                {
                    var problem = new AppProblemDetails(
                        title: "Bad Request",
                        detail: appException.Message,
                        statusCode: StatusCodes.Status400BadRequest,
                        errorCode: appException.ErrorCode
                    );

                    context.Response.StatusCode = problem.Status ?? StatusCodes.Status400BadRequest;
                    await WriteProblemDetails(context, problem);
                }
                else
                {
                    var problem = new AppProblemDetails(
                        title: "Internal Server Error",
                        detail: "An unexpected error occurred.",
                        statusCode: StatusCodes.Status500InternalServerError,
                        errorCode: "INTERNAL_ERROR"
                    );

                    context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
                    await WriteProblemDetails(context, problem);
                }
            }
        }

        private async Task WriteProblemDetails(HttpContext context, AppProblemDetails problem)
        {
            var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);

        }
    }
}
