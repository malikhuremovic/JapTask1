using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories.Repositories
{
    public class ProgramItemRepository : IProgramItemRepository
    {
        private readonly DataContext _context;
        public ProgramItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramItem>> GetProgramItemsAsync(int programId)
        {
            return await _context.ProgramItems
           .Where(pt => pt.ProgramId == programId)
           .OrderBy(pt => pt.Order)
           .Include(pt => pt.Item)
           .ToListAsync();
        }

        public async Task ModifyProgramItemsOrderAsync(List<ProgramItem> programItems)
        {
            _context.ProgramItems.UpdateRange(programItems);
            await _context.SaveChangesAsync();
        }
    }
}
