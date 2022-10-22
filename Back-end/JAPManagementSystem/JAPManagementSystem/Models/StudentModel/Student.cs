using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.SelectionModel;
using JAPManagementSystem.Models.UserModel;

namespace JAPManagementSystem.Models.StudentModel
{
    public class Student : User
    {
        public StudentStatus Status { get; set; } = StudentStatus.InProgram;
        public int? SelectionId { get; set; }
        public Selection? Selection { get; set; }
        public List<Comment> Comments { get; set; }
        public List<JapItem> Items { get; set; } = new List<JapItem>();
        public List<StudentItem> StudentItems { get; set; } = new List<StudentItem>();
    }
}
