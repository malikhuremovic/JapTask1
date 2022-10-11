using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Models.UserModel;

namespace JAPManagementSystem.DTOs.StudentDto
{
    public class GetStudentDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; } = UserRole.Student;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public StudentStatus Status { get; set; }
        public GetSelectionDto? Selection { get; set; }
        public List<GetCommentDto> Comments { get; set; }
    }
}
