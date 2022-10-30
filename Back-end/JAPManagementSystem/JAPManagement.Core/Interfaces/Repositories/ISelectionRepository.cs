using EntityFrameworkPaginate;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.SelectionModel;

namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface ISelectionRepository : BaseRepository<Selection, int>
    {
        Page<Selection> GetSelectionsWithParams(int pageNumber, int pageSize, Sorts<Selection> sorts, Filters<Selection> filters);
        Task<Selection> GetByName(string name);
        Task<Selection> GetByIdWithProgramAsync(int id);
        Task<List<AdminReport>> GetAdminReportAsync();
        Task<List<Selection>> GetByEndMonthAndDay(int endMonth, int endDay);
    }
}
