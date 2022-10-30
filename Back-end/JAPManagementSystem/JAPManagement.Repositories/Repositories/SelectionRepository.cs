using EntityFrameworkPaginate;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.SelectionModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories.Repositories
{
    public class SelectionRepository : ISelectionRepository
    {
        private readonly DataContext _context;
        public SelectionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Selection> Add(Selection obj)
        {
            _context.Selections.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<Selection> Delete(int id)
        {
            var selection = await _context.Selections.FirstOrDefaultAsync(selection => selection.Id == id);
            if (selection != null)
            {
                _context.Selections.Remove(selection);
                await _context.SaveChangesAsync();
            }
            return selection;
        }

        public async Task<List<AdminReport>> GetAdminReportAsync()
        {
            return await _context.AdminReport.FromSqlRaw("GetSelectionSuccessRate").ToListAsync();
        }

        public async Task<List<Selection>> GetAllAsync()
        {
            return await _context.Selections.ToListAsync();
        }

        public async Task<Selection> GetByIdAsync(int id)
        {
            return await _context.Selections.FirstOrDefaultAsync(selection => selection.Id == id);
        }

        public async Task<Selection> GetByIdWithProgramAsync(int id)
        {
            return await _context.Selections.Where(s => s.Id == id).Include(s => s.JapProgram).FirstAsync();
        }

        public async Task<Selection> GetByName(string name)
        {
            return await _context.Selections.Where(selection => selection.Name.Equals(name)).Include(s => s.JapProgram).FirstAsync();
        }

        public Page<Selection> GetSelectionsWithParams(int pageNumber, int pageSize, Sorts<Selection> sorts, Filters<Selection> filters)
        {
            return _context.Selections.Include(s => s.JapProgram)
                .Paginate(
                pageNumber,
                pageSize,
                sorts,
                filters);
        }

        public async Task<Selection> Update(Selection obj)
        {
            var selection = await _context.Selections.FirstOrDefaultAsync(selections => selections.Id == obj.Id);
            if (selection != null)
            {
                selection.Name = obj.Name;
                selection.DateStart = obj.DateStart;
                selection.DateEnd = obj.DateEnd;
                selection.Status = obj.Status;
                selection.JapProgramId = obj.JapProgramId;
            }
            await _context.SaveChangesAsync();
            return selection;
        }
    }
}
