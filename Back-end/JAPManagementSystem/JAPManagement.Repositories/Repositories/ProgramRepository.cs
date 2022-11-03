using EntityFrameworkPaginate;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly DataContext _context;
        public ProgramRepository(DataContext context, IItemRepository itemRepository)
        {
            _context = context;
        }
        public async Task<JapProgram> Add(JapProgram program)
        {
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] ON");
            _context.JapPrograms.Add(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<JapProgram> Delete(int id)
        {
            var program = await _context.JapPrograms.FirstOrDefaultAsync(program => program.Id == id);
            if (program != null)
            {
                _context.JapPrograms.Remove(program);
                await _context.SaveChangesAsync();
            }
            return program;
        }

        public async Task<List<JapProgram>> GetAllAsync()
        {
            return await _context.JapPrograms.ToListAsync();
        }

        public async Task<List<JapProgram>> GetAllWithItemsAsync()
        {
            return await _context.JapPrograms.Include(p => p.Items).ToListAsync();
        }

        public async Task<JapProgram> GetByIdAsync(int id)
        {
            return await _context.JapPrograms.FirstOrDefaultAsync(program => program.Id == id);
        }

        public async Task<JapProgram> GetByIdWithItemsAsync(int id)
        {
            return await _context.JapPrograms.Include(j => j.Items).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Page<JapProgram> GetProgramsWithParams(int pageNumber, int pageSize, Sorts<JapProgram> sorts, Filters<JapProgram> filters)
        {
            return _context.JapPrograms
                .Paginate(
                pageNumber,
                pageSize,
                sorts,
                filters);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<JapProgram> Update(JapProgram obj)
        {
            var program = await _context.JapPrograms.Include(p => p.Items).FirstOrDefaultAsync(program => program.Id == obj.Id);
            if (program != null)
            {
                program.Name = obj.Name;
                program.Content = obj.Content;
            }
            await _context.SaveChangesAsync();
            return program;
        }
    }
}
