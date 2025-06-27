namespace MyFlipKart.DomainModels
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Code { get; set; }

        public static ApiResponse<T> Success(T? data, string message = "Success", string code = "SUCCESS", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Code = code
            };
        }

        public static ApiResponse<T> Fail(string message, string code = "ERROR", int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = default,
                Code = code
            };
        }
    }

}
