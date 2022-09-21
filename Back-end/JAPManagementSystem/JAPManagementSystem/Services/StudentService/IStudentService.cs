using JAPManagementSystem.DTOs;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent);
        Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents();
        Task<ServiceResponse<List<GetStudentDto>>> GetStudentByName(string studentName);
        Task<ServiceResponse<GetStudentDto>> ModifyStudent(ModifyStudentDto modifiedStudent);
        Task<ServiceResponse<string>> DeleteStudent(int studentId);


    }
}
