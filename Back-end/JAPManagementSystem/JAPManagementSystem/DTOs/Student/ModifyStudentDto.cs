using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Student
{
    public class ModifyStudentDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public StudentStatus Status { get; set; }
        public int? SelectionId { get; set; } = null;
    }
}
