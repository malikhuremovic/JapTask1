namespace JAPManagementSystem.Models.ProgramModel
{
    public class JapProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<JapItem> Items { get; set; } = new List<JapItem>();
        public List<ProgramItem> ProgramItems { get; set; } = new List<ProgramItem>();
    }
}
