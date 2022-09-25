namespace JAPManagementSystem.DTOs.Comment
{
    public class AddCommentDto
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StudentId { get; set; }
    }
}
