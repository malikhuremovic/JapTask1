using JAPManagement.Core.Models.SelectionModel;

namespace JAPManagement.Core.DTOs.SelectionDTOs
{
    public class ModifySelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public SelectionStatus Status { get; set; }
        public int JapProgramId { get; set; }

    }
}
