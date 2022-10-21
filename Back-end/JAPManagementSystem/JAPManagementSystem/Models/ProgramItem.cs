namespace JAPManagementSystem.Models
{
    public class ProgramItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int ProgramId { get; set; }
        public JapProgram Program { get; set; }
        public int Order { get; set; }
    }
}
