using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.DTOs.JapItemDTOs
{
    public class ModifyStudentItemDto
    {
        public int ItemId { get; set; }
        public int Done { get; set; }
        public StudentItemStatus Status { get; set; }
    }
}
