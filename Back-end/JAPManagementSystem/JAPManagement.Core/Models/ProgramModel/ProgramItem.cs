namespace JAPManagement.Core.Models.ProgramModel
{
    public class ProgramItem
    {
        public int ItemId { get; set; }
        public JapItem Item { get; set; }
        public int ProgramId { get; set; }
        public JapProgram Program { get; set; }
        public int Order { get; set; }
    }
}
