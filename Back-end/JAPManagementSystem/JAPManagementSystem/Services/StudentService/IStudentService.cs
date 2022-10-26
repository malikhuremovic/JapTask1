using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.StudentDTOs;
using JAPManagementSystem.Models.Response;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent);
        Task<ServiceResponse<List<GetCommentDto>>> AddComment(AddCommentDto newComment);
        Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents();
        Task<ServiceResponse<GetStudentDto>> GetStudentByToken(string token);
        Task<ServiceResponse<StudentPersonalProgram>> GetStudentPersonalProgram(string studentId);
        ServiceResponse<GetStudentPageDto> GetStudentsWithParams(int page, int pageSize, string? firstName, string? lastName, string? email, string? selectionName, string? japProgramName, StudentStatus? status, string sort, bool descending);
        Task PopulateStudentItems(string studentId);
        Task<ServiceResponse<GetStudentDto>> GetStudentById(string id);
        Task<ServiceResponse<GetStudentItemDto>> ModifyStudentItem(string studentId, ModifyStudentItemDto modifiedItem);
        Task<ServiceResponse<GetStudentDto>> ModifyStudent(ModifyStudentDto modifiedStudent);
        Task<ServiceResponse<string>> DeleteStudent(string studentId);
    }
}
