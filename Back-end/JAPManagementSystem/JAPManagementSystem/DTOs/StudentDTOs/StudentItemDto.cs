using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.DTOs.StudentDTOs
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
