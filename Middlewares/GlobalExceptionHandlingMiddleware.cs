using FluentValidation;
using RegisterService.Exceptions;
using System.Data;
using System.Data.Entity.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RegisterService.Middlewares
{
    //public class GlobalExceptionHandlingMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    //    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    //    {
    //        _next = next;
    //        _logger = logger;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        try
    //        {
    //            await _next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Unhandled exception");
    //            context.Response.ContentType = "application/problem+json";

    //            AppProblemDetails problem;

    //            switch (ex)
    //            {
    //                case ValidationException validationEx:
    //                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
    //                    problem = new AppProblemDetails(
    //                        title: "Validation error",
    //                        detail: "One or more validation errors occurred.",
    //                        statusCode: StatusCodes.Status400BadRequest,
    //                        errorCode: "VALIDATION_ERROR");

    //                    problem.Extensions["errors"] = validationEx.Errors
    //                        .GroupBy(e => e.PropertyName)
    //                        .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
    //                    break;

    //                case AppException appEx:
    //                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
    //                    problem = new AppProblemDetails(
    //                        title: "Bad Request",
    //                        detail: appEx.Message,
    //                        statusCode: StatusCodes.Status400BadRequest,
    //                        errorCode: appEx.ErrorCode ?? "APP_ERROR");
    //                    break;

    //                case NotFoundException notFoundEx:
    //                    context.Response.StatusCode = StatusCodes.Status404NotFound;
    //                    problem = new AppProblemDetails(
    //                        title: "Not Found",
    //                        detail: notFoundEx.Message,
    //                        statusCode: StatusCodes.Status404NotFound,
    //                        errorCode: "NOT_FOUND");
    //                    break;

    //                case UnauthorizedAccessException:
    //                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                    problem = new AppProblemDetails(
    //                        title: "Unauthorized",
    //                        detail: "Access is denied.",
    //                        statusCode: StatusCodes.Status401Unauthorized,
    //                        errorCode: "UNAUTHORIZED");
    //                    break;
    //                case EntityException:
    //                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //                    problem = new AppProblemDetails(
    //                        title: "Database Error",
    //                        detail: "An error occurred while accessing the database.",
    //                        statusCode: StatusCodes.Status500InternalServerError,
    //                        errorCode: "DATABASE_ERROR");
    //                    break;

    //                default:
    //                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //                    problem = new AppProblemDetails(
    //                        title: "Internal Server Error",
    //                        detail: "An unexpected error occurred.",
    //                        statusCode: StatusCodes.Status500InternalServerError,
    //                        errorCode: "INTERNAL_ERROR");
    //                    break;
    //            }

    //            problem.Extensions["traceId"] = context.TraceIdentifier;

    //            await WriteProblemDetails(context, problem);
    //        }
    //    }
    //    private static readonly JsonSerializerOptions _jsonOptions = new()
    //    {
    //        WriteIndented = true,
    //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    //    };
    //    private async Task WriteProblemDetails(HttpContext context, AppProblemDetails problem)
    //    {
    //        try
    //        {
    //            var json = JsonSerializer.Serialize(problem, _jsonOptions);
    //            await context.Response.WriteAsync(json);
    //        }
    //        catch (Exception serializationEx)
    //        {
    //            _logger.LogError(serializationEx, "Failed to serialize problem details");
    //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //            await context.Response.WriteAsync("{\"title\":\"Internal Server Error\"}");
    //        }
    ////    }
    //}
}