using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.DTOs.JapItemDTOs
{
    public class ModifyStudentItemDto
    {
        public int ItemId { get; set; }
        public int Done { get; set; }
        public StudentItemStatus Status { get; set; }
    }
}
