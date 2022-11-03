using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.DTOs.StudentDTOs
{
    public class AddStudentItemDto
    {
        public string StudentId { get; set; }
        public int ItemId { get; set; }
        public int Done { get; set; }
        public int ExpectedHours { get; set; }
        public StudentItemStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
