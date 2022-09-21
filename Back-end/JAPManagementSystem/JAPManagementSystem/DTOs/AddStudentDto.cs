using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs
{
    public class AddStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int? SelectionId { get; set; } = null;
    }
}
