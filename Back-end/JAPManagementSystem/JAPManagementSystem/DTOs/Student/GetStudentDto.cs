using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Student
{
    public class GetStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public StudentStatus Status { get; set; }
        public GetSelectionDto? Selection { get; set; }
    }
}
