namespace JAPManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int? SelectionId { get; set; }
        public Selection Selection { get; set; }
    }
}
