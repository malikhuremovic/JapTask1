namespace JAPManagement.Core.DTOs.JapItemDTOs
{
    public class GetItemPageDto
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public List<GetItemDto> Results { get; set; }
    }
}
