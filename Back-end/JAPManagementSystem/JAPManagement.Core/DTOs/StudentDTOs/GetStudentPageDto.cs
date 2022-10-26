namespace JAPManagement.Core.DTOs.StudentDTOs
{
    public class GetStudentPageDto
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public List<GetStudentDto> Results { get; set; }
    }
}
