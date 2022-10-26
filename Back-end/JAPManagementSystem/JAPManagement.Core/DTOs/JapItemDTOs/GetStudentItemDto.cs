using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.DTOs.JapItemDTOs
{
    public class GetStudentItemDto
    {
        public int ItemId { get; set; }
        public int Done { get; set; }
        public StudentItemStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
