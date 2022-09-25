using JAPManagementSystem.DTOs.Student;

namespace JAPManagementSystem.DTOs.Comment
{
    public class GetCommentDto
    {
        public string Text { get; set; }
        public int StudentId { get; set; }
        public GetStudentDto Student { get; set; }
    }
}
