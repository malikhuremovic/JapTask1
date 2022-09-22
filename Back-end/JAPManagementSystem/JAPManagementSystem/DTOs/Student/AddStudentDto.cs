using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Student
{
    public class AddStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int? SelectionId { get; set; } = null;
    }
}
