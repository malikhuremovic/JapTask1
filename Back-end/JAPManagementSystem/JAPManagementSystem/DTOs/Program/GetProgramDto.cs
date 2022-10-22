using JAPManagementSystem.DTOs.JapItemDTOs;

namespace JAPManagementSystem.DTOs.Program
{
    public class GetProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<GetItemDto> Items { get; set; } = new List<GetItemDto>();
    }
}
