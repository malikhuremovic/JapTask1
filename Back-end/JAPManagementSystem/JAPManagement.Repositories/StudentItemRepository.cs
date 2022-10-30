using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.StudentModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories
{
    public class StudentItemRepository : IStudentItemRepository
    {
        private readonly DataContext _context;
        public StudentItemRepository(DataContext context)        {
            _context = context;
        }

        public async Task<List<StudentItem>> AddRange(List<StudentItem> studentItems)
        {
            _context.StudentItems.AddRange(studentItems);
            await _context.SaveChangesAsync();
            return studentItems;
        }

        public async Task<List<StudentItem>> DeleteRange(List<StudentItem> studentItems)
        {
            _context.StudentItems.RemoveRange(studentItems);
            await _context.SaveChangesAsync();
            return studentItems;
        }

        public async Task<List<StudentItem>> GetByStudentIdAsync(string id)
        {
            return await _context.StudentItems.Where(st => st.StudentId.Equals(id)).Include(st => st.Item).ToListAsync();
        }

        public async Task<List<StudentItem>> UpdateRange(List<StudentItem> studentItems)
        {
            return studentItems;
        }
    }
}
