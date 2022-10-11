using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SId { get; set; }
        public Student Student { get; set; }
    }
}
