using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.Comment;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.StudentModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Student> Add(Student obj)
        {
            _context.Students.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Comment>> AddStudentComment(Comment newComment)
        {
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            return await _context.Comments
                            .Where(c => c.SId
                            .Equals(newComment.SId))
                            .OrderBy(c => c.CreatedAt)
                            .ToListAsync();
        }

        public async Task<Student> Delete(string id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(student => student.Id.Equals(id));
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>> GetAllWithSelectionAndProgram()
        {
            return await _context.Students.Include(s => s.Selection).ThenInclude(s => s.JapProgram).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await _context.Students.Where(student => student.Id.Equals(id)).FirstAsync();
        }

        public async Task<List<Student>> GetByIdInAsync(List<string> ids)
        {
            return await _context.Students
                            .Where(u => ids
                            .Contains(u.Id))
                            .ToListAsync();
        }

        public async Task<Student> GetByIdWithCommentAndSelectionAndProgram(string id)
        {
            return await _context.Students.Where(student => student.Id.Equals(id))
                .Include(s => s.Comments)
                .Include(s => s.Selection)
                .ThenInclude(s => s.JapProgram)
                .FirstAsync();
        }

        public async Task<Student> GetByIdWithSelectionAndProgram(string id)
        {
            return await _context.Students.Where(student => student.Id.Equals(id))
                .Include(s => s.Selection)
                .ThenInclude(s => s.JapProgram)
                .FirstAsync();
        }

        public Page<Student> GetStudentsWithParams(int pageNumber, int pageSize, Sorts<Student> sorts, Filters<Student> filters)
        {
            return _context.Students
                            .Include(s => s.Selection)
                            .ThenInclude(s => s.JapProgram)
                            .Paginate(
                            pageNumber,
                            pageSize,
                            sorts,
                            filters);
        }

        public async Task<Student> Update(Student obj)
        {
            var student = await _context.Students.Include(s => s.Selection).ThenInclude(s => s.JapProgram).FirstOrDefaultAsync(students => students.Id.Equals(obj.Id));
            if (student != null)
            {
                student.FirstName = obj.FirstName;
                student.LastName = obj.LastName;
                student.Email = obj.Email;
                student.Status = obj.Status;
                student.SelectionId = obj.SelectionId;
            }
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
