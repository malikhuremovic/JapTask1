using EntityFrameworkPaginate;
using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface IProgramRepository : BaseRepository<JapProgram, int>
    {
        Task<List<JapProgram>> GetAllWithItemsAsync();
        Task<JapProgram> GetByIdWithItemsAsync(int id);
        Task SaveChangesAsync();
        Page<JapProgram> GetProgramsWithParams(int pageNumber, int pageSize, Sorts<JapProgram> sorts, Filters<JapProgram> filters);
    }
}
