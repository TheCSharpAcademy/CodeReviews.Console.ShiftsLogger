namespace HKhemanthSharma.ShiftLoggerAPI.Model.Dto
{
    public class ResponseDto<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
        public static ResponseDto<T> Success(T Data, string Message)
        {
            return new ResponseDto<T>
            {
                Data = Data,
                IsSuccess = true,
                Message = Message,
                Errors = new List<string>(),
                ResponseTime = DateTime.Now
            };
        }
        public static ResponseDto<T> Failure(T Data, string ErrorMessage)
        {
            return new ResponseDto<T>
            {
                Data = default,
                IsSuccess = false,
                Message = ErrorMessage,
                Errors = new List<string>(),
                ResponseTime = DateTime.Now
            };
        }
    }
}
