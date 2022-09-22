using JAPManagementSystem.Models;

namespace JAPManagementSystem.DTOs.Selection
{
    public class ModifySelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? JapProgramId { get; set; }
        public List<int>? StudentsToRemove { get; set; } = new List<int>();
        public List<int>? StudentsToAdd { get; set; } = new List<int>();

    }
}
