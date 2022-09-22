namespace JAPManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public StudentStatus Status { get; set; } = StudentStatus.InProgram;
        public int? SelectionId { get; set; } = null;
        public Selection? Selection { get; set; } = null;
    }
}
