using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs
{
    public class GetStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public GetSelectionDto? Selection { get; set; }
    }
}
