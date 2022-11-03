using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.Models.SelectionModel;

namespace JAPManagement.Core.DTOs.SelectionDTOs
{
    public class GetSelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public SelectionStatus Status { get; set; }
        public DateTime DateEnd { get; set; }
        public GetProgramDto JapProgram { get; set; }
        public int SuccessRate { get; set; }
        public ICollection<GetStudentDto> Students { get; set; } = new List<GetStudentDto>();
    }
}
