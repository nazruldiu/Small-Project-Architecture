namespace ArchitectureWebAPI.DTO
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {
        }
        public ResponseDto(T data, bool succeeded, string message, List<string>? errors)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors;
            Data = data;
        }
        public bool Succeeded { get; set; }
        public List<string>? Errors { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
