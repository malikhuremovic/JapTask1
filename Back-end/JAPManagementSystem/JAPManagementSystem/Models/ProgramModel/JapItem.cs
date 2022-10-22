using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.Models.ProgramModel
{
    public class JapItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int ExpectedHours { get; set; }
        public bool IsEvent { get; set; }
        public List<JapProgram> Programs { get; set; } = new List<JapProgram>();
        public List<ProgramItem> ProgramItems { get; set; } = new List<ProgramItem>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<StudentItem> StudentItems { get; set; } = new List<StudentItem>();
    }
}
