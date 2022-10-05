using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent);
        Task<ServiceResponse<List<GetCommentDto>>> AddComment(AddCommentDto newComment);
        Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents();
        Task<ServiceResponse<GetStudentDto>> GetStudentById(int id);
        ServiceResponse<GetStudentPageDto> GetStudentsWithParams(int page, int pageSize, string? firstName, string? lastName, string? email, string? selectionName, string? japProgramName, StudentStatus? status, string sort, bool descending);
        Task<ServiceResponse<GetStudentDto>> ModifyStudent(ModifyStudentDto modifiedStudent);
        Task<ServiceResponse<string>> DeleteStudent(int studentId);
    }
}
