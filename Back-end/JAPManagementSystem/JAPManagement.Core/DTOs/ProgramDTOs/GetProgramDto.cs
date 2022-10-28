using JAPManagement.Core.DTOs.JapItemDTOs;

namespace JAPManagement.Core.DTOs.Program
{
    public class GetProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<GetItemDto> Items { get; set; } = new List<GetItemDto>();
    }
}
