namespace JAPManagementSystem.Models
{
    public class Selection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public JapProgram JapProgram { get; set; }
        public List<Student> Students { get; set; }
    }
}
