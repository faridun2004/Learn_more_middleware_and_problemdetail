using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace RegisterService.Exceptions
{
    public class AppProblemDetails: ProblemDetails
    {
        [JsonPropertyName("ErrorCode")]
        public string ErrorCode { get; set; }
        public Dictionary<string, object> Extensions { get; } = new();
        public AppProblemDetails(string title, string detail,int? statusCode, string errorCode) 
        {
            Title = title;
            Detail = detail;
            Status=statusCode;
            ErrorCode = errorCode;
            Type = $"https://httpstatuses.com/{statusCode}";
        }
    }
}
