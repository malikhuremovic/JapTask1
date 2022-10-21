namespace JAPManagementSystem.Models
{
    public class JapProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<ProgramItem> ProgramItem { get; set; } = new List<ProgramItem>();
    }
}
