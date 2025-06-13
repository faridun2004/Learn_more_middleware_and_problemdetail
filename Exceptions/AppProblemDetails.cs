using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace RegisterService.Exceptions
{
    public class AppProblemDetails : ProblemDetails
    {
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }
        public AppProblemDetails(string title, string detail, int? statusCode, string errorCode)
        {
            Title = title;
            Detail = detail;
            Status = statusCode;
            ErrorCode = errorCode;
            Type = $"https://httpstatuses.com/{statusCode}";
        }
    }
}
