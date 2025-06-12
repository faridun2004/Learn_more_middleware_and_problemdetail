namespace RegisterService.Exceptions
{
    public class AppException : Exception
    {
        public string ErrorCode { get; }
        public AppException(string message, string errorCode = "APP_ERROR"): base(message) 
        {
            ErrorCode = errorCode;
        }
    }
}
