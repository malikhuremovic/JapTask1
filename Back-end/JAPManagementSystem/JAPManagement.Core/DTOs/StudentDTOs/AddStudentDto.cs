using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.DTOs.StudentDTOs
{
    public class AddStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public StudentStatus Status { get; set; }
        public int? SelectionId { get; set; } = null;
    }
}
