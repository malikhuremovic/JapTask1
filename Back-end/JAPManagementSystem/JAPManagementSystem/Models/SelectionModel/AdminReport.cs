using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAPManagementSystem.Models.SelectionModel
{
    public class AdminReport
    {
        public string SelectionName { get; set; }
        public string ProgramName { get; set; }
        public int SelectionSuccessRate { get; set; }
        public int OverallSuccessRate { get; set; }
    }
}
