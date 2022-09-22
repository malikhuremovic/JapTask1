using JAPManagementSystem.DTOs.Student;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent);
        Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents();
        ServiceResponse<List<GetStudentDto>> GetStudentsWithParams(int page, int pageSize, string? firstName, string? lastName, string? email, string? selectionName, int sort);
        Task<ServiceResponse<GetStudentDto>> ModifyStudent(ModifyStudentDto modifiedStudent);
        Task<ServiceResponse<string>> DeleteStudent(int studentId);
    }
}
