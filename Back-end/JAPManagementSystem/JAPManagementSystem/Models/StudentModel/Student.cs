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
    }
}
