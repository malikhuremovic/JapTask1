using JAPManagementSystem.Models.SelectionModel;

namespace JAPManagementSystem.DTOs.Selection
{
    public class AddSelectionDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public SelectionStatus Status { get; set; } = SelectionStatus.Active;

        public int? JapProgramId { get; set; }
        public List<string>? StudentIds { get; set; } = new List<string>();
    }
}
