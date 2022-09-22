namespace JAPManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public StudentStatus Status { get; set; } = StudentStatus.InProgram;
        public int? SelectionId { get; set; }
        public Selection Selection { get; set; }
    }
}
