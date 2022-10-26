using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.DTOs.StudentDTOs
{
    public class StudentPersonalProgram
    {
        public Student Student { get; set; }
        public List<GetItemDto> ProgramItems { get; set; }
        public List<StudentItem> StudentItems { get; set; }
    }
}
