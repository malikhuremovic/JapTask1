using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.DTOs.StudentDTOs
{
    public class StudentItemDto
    {
        public string StudentId { get; set; }
        public GetItemDto Item { get; set; }
        public int ItemId { get; set; }
        public int Done { get; set; }
        public StudentItemStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
