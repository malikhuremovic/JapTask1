namespace JAPManagementSystem.DTOs
{
    public class ModifyStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int? SelectionId { get; set; } = null;
    }
}
