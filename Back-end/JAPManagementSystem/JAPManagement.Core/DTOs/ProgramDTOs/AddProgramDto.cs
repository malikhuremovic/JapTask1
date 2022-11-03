namespace JAPManagement.Core.DTOs.Program
{
    public class AddProgramDto
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<int> Lectures { get; set; }
    }
}
