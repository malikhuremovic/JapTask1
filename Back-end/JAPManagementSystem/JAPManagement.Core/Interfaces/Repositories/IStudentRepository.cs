using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.Comment;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface IStudentRepository : BaseRepository<Student, string>
    {
        Task<Student> GetByIdWithSelectionAndProgram(string id);
        Task<List<Student>> GetAllWithSelectionAndProgram();
        Task<List<Student>> GetByIdInAsync(List<string> ids);
        Task<Student> GetByIdWithCommentAndSelectionAndProgram(string id);
        Page<Student> GetStudentsWithParams(int pageNumber, int pageSize, Sorts<Student> sorts, Filters<Student> filters);
        Task<List<Comment>> AddStudentComment(Comment newComment);
    }
}
