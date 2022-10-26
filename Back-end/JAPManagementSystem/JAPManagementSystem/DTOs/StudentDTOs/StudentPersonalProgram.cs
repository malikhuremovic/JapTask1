using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.DTOs.StudentDTOs
{
    public class StudentPersonalProgram
    {
        public string Name { get; set; }
        public int ExpectedHours { get; set; }
        public bool isEvent { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string URL { get; set; }
        public StudentItemStatus Status { get; set; }
        public int Done { get; set; }
        public int Order { get; set; }
    }
}
