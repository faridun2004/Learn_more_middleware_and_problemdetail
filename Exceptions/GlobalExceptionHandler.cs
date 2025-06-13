using Microsoft.AspNetCore.Diagnostics;
using System.Data.Entity.Core;
using System.Text.Json.Serialization;
using System.Text.Json;
using FluentValidation;

namespace RegisterService.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception");

            context.Response.ContentType = "application/problem+json";

            AppProblemDetails problem = exception switch
            {
                ValidationException validationEx => CreateValidationProblem(context, validationEx),
                AppException appEx => new AppProblemDetails("Bad Request", appEx.Message, StatusCodes.Status400BadRequest, appEx.ErrorCode ?? "APP_ERROR")
                {
                    Status = StatusCodes.Status400BadRequest
                },
                NotFoundException notFoundEx => new AppProblemDetails("Not Found", notFoundEx.Message, StatusCodes.Status404NotFound, "NOT_FOUND")
                {
                    Status = StatusCodes.Status404NotFound
                },
                UnauthorizedAccessException => new AppProblemDetails("Unauthorized", "Access is denied.", StatusCodes.Status401Unauthorized, "UNAUTHORIZED")
                {
                    Status = StatusCodes.Status401Unauthorized
                },
                EntityException => new AppProblemDetails("Database Error", "An error occurred while accessing the database.", StatusCodes.Status500InternalServerError, "DATABASE_ERROR")
                {
                    Status = StatusCodes.Status500InternalServerError
                },
                _ => new AppProblemDetails("Internal Server Error", "An unexpected error occurred.", StatusCodes.Status500InternalServerError, "INTERNAL_ERROR")
                {
                    Status = StatusCodes.Status500InternalServerError
                }
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            var json = JsonSerializer.Serialize(problem, _jsonOptions);
            context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(json, cancellationToken);

            return true;
        }

        private static AppProblemDetails CreateValidationProblem(HttpContext context, ValidationException validationEx)
        {
            var problem = new AppProblemDetails(
                title: "Validation error",
                detail: "One or more validation errors occurred.",
                statusCode: StatusCodes.Status400BadRequest,
                errorCode: "VALIDATION_ERROR");

            problem.Extensions["errors"] = validationEx.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            return problem;
        }
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

}