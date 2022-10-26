namespace JAPManagement.Core.DTOs.Program
{
    public class GetProgramPageDto
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public List<GetProgramDto> Results { get; set; }
    }
}
