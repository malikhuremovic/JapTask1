using EntityFrameworkPaginate;
using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface IItemRepository : BaseRepository<JapItem, int>
    {
        Page<JapItem> GetItemsWithParams(int pageNumber, int pageSize, Sorts<JapItem> sorts, Filters<JapItem> filters);
        Task<List<JapItem>> GetByIdInAsync(List<int> ids);
    }
}
