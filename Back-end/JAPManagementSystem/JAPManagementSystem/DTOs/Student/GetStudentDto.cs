using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Student
{
    public class GetStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public StudentStatus Status { get; set; }
        public GetSelectionDto? Selection { get; set; }
    }
}
