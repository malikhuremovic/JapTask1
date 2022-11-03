using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Core.DTOs.SelectionDTOs
{
    public class SelectionReportDto
    {
        public string SelectionName { get; set; } = string.Empty;
        public int? JapProgramId { get; set; }
        public JapProgram? JapProgram { get; set; }
        public string JapProgramName { get; set; }
        public int OverallSuccessRate { get; set; }
        public int? SuccessRate { get; set; }
    }
}
