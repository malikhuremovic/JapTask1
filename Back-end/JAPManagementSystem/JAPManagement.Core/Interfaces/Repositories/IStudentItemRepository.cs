using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface IStudentItemRepository
    {
        Task<List<StudentItem>> AddRange(List<StudentItem> studentItems);
        Task<List<StudentItem>> UpdateRange(List<StudentItem> studentItems);
        Task<List<StudentItem>> GetByStudentIdAsync(string id);
        Task<List<StudentItem>> DeleteRange(List<StudentItem> studentItems);
    }
}
