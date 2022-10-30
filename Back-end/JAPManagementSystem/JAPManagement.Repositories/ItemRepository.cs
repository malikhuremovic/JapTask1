using EntityFrameworkPaginate;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<JapItem> Add(JapItem japItem)
        {
            _context.Items.Add(japItem);
            await _context.SaveChangesAsync();
            return japItem;
        }

        public async Task<JapItem> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);
            if(item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }
        public async Task<JapItem> Update(JapItem japItem)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == japItem.Id);
            if (item != null)
            {
                item.Name = japItem.Name;
                item.URL = japItem.URL;
                item.Description = japItem.Description;
                item.ExpectedHours = japItem.ExpectedHours;
                item.IsEvent = japItem.IsEvent;
            }
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<JapItem>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<JapItem> GetByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(item => item.Id == id);
        }

        public Page<JapItem> GetItemsWithParams(int pageNumber, int pageSize, Sorts<JapItem> sorts, Filters<JapItem> filters)
        {
            return _context.Items
                .Paginate(
                pageNumber,
                pageSize,
                sorts,
                filters);
        }

        public async Task<List<JapItem>> GetByIdInAsync(List<int> ids)
        {
            return await _context.Items.Where(l => ids.Contains(l.Id)).ToListAsync();
        }
    }
}
