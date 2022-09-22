using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Selection
{
    public class AddSelectionDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? JapProgramId { get; set; }
        public List<int>? StudentIds { get; set; } = new List<int>();
    }
}
