using JAPManagementSystem.Models.ProgramModel;

namespace JAPManagementSystem.Models.StudentModel
{
    public class StudentItem
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public int ItemId { get; set; }
        public JapItem Item { get; set; }
        public int Done { get; set; }
        public StudentItemStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
