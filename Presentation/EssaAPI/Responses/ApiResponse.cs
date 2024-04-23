namespace EssaAPI.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string[] Errors { get; set; }
        public int StatusCode { get; set; }

        // Default constructor
        public ApiResponse()
        {
        }

        // Constructor with data, status code and optional errors
        public ApiResponse(T data, int statusCode, params string[] errors)
        {
            Data = data;
            StatusCode = statusCode;
            Errors = errors;
        }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>(data, 200);
        }

        public static ApiResponse<T> Error(int statusCode, params string[] errors)
        {
            return new ApiResponse<T>(default, statusCode, errors);
        }
    }
}
