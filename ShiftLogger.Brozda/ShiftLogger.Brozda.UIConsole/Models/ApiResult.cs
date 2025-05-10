namespace ShiftLogger.Brozda.UIConsole.Models
{
    /// <summary>
    /// Model representing result of an API call
    /// </summary>
    /// <typeparam name="T">Represent data type expected in the API response</typeparam>
    internal class ApiResult<T>
    {
        public bool IsSuccessful { get; set; }

        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }

        public T? Data { get; set; }

        /// <summary>
        /// Initalizes a <see cref="ApiResult{T}"/> object containing all respetive data in case of success response
        /// </summary>
        /// <param name="model">Model containing data received from API</param>
        /// <returns><see cref="ApiResult{T}"/> object containing all respetive data</returns>
        public static ApiResult<T> Success(T model)
        {
            return new ApiResult<T>()
            {
                IsSuccessful = true,
                StatusCode = 200,
                Data = model
            };
        }

        /// <summary>
        /// Initalizes a <see cref="ApiResult{T}"/> object containing all respetive data in case of HTTP not found response
        /// </summary>
        /// <returns><see cref="ApiResult{T}"/> object containing all respetive data</returns>
        public static ApiResult<T> NotFound()
        {
            return new ApiResult<T>()
            {
                IsSuccessful = false,
                StatusCode = 404,
                ErrorMessage = "Your request did not match any record in the database",
            };
        }

        /// <summary>
        /// Initalizes a <see cref="ApiResult{T}"/> object containing all respetive data in case of HTTP error (5xx status code)
        /// </summary>
        /// <param name="message">Message to be contained in the object </param>
        /// <param name="statusCode">HTTP status code</param>
        /// <returns><see cref="ApiResult{T}"/> object containing all respetive data</returns>
        public static ApiResult<T> Fail(string message, int statusCode = 500)
        {
            return new ApiResult<T>()
            {
                IsSuccessful = false,
                StatusCode = statusCode,
                ErrorMessage = message,
            };
        }

        /// <summary>
        /// Initalizes a <see cref="ApiResult{T}"/> in case of invalid data passes in API request
        /// </summary>
        /// <returns><see cref="ApiResult{T}"/> object containing error message about invalid data</returns>
        public static ApiResult<T> InvalidData()
        {
            return new ApiResult<T>()
            {
                IsSuccessful = false,
                Data = default(T),
                ErrorMessage = "Invalid data passed"
            };
        }
    }
}