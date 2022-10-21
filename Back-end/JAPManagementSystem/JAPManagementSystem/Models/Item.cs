namespace JAPManagementSystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int ExpectedHours { get; set; }
        public bool isEvent { get; set; }
        public List<JapProgram> Programs { get; set; } = new List<JapProgram>();
        public List<ProgramItem> ProgramItem { get; set; } = new List<ProgramItem>();
    }
}
