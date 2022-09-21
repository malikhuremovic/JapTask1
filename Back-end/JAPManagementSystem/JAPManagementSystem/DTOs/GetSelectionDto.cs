using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs
{
    public class GetSelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public JapProgram JapProgram { get; set; }
        public ICollection<GetStudentDto> Students { get; set; } = new List<GetStudentDto>();
    }
}
