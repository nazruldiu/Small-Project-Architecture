namespace ArchitectureWebAPI.DTO
{
    public class PagingResponseDto<T> : ResponseDto<T>
    {
        public int TotalRecords { get; set; }
        public int Showing { get; set; }
        public PagingResponseDto(T data, int totalRecords, int recordPerPage, bool succeeded, string message, List<string>? errors)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors;
            Data = data;
            TotalRecords = totalRecords;
            Showing = recordPerPage;
        }
    }
}
