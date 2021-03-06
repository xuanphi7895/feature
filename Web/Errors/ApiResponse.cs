namespace Web.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);

        }
      
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                200 => "Get success",
                201 => "Create new object",
                400 => "A bad request",
                401 => "Authorized, you are not",
                404 => "Not found, please check url",
                500 => "Server error",
                _  => null
            };
        }
    }
}