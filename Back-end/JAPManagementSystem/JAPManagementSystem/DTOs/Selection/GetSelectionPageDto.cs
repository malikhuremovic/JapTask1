using JAPManagementSystem.DTOs.StudentDto;

namespace JAPManagementSystem.DTOs.Selection
{
    public class GetSelectionPageDto
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public List<GetSelectionDto> Results { get; set; }
    }
}
