using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.Models.SelectionModel
{
    public class Selection
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public SelectionStatus Status { get; set; }
        public int? JapProgramId { get; set; }
        public JapProgram? JapProgram { get; set; }
        public ICollection<Student>? Students { get; set; } = new List<Student>();
    }
}
