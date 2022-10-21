using JAPManagementSystem.DTOs.LectureDTOs;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Program
{
    public class GetProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<GetItemDto> Lectures { get; set; } = new List<GetItemDto>();
    }
}
