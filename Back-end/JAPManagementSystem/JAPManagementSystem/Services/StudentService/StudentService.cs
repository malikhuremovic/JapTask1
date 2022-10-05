using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace JAPManagementSystem.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public StudentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            try
            {
                var student = _mapper.Map<Student>(newStudent);
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                var fetchedStudent = await _context.Students.Include(s => s.Selection).ThenInclude(s => s.JapProgram).FirstOrDefaultAsync();
                response.Data = _mapper.Map<GetStudentDto>(fetchedStudent);
                response.Message = "You have successfully added a new student";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents()
        {
            ServiceResponse<List<GetStudentDto>> response = new ServiceResponse<List<GetStudentDto>>();
            try
            {
                var students = await _context.Students.Include(c => c.Comments).Include(s => s.Selection).ThenInclude(s => s.JapProgram).OrderByDescending(s => s.FirstName).ToListAsync();
                response.Data = students.Select(s => _mapper.Map<GetStudentDto>(s)).ToList();
                response.Message = "You have fetched all the students in the database.";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
        public async Task<ServiceResponse<GetStudentDto>> ModifyStudent(ModifyStudentDto modifiedStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == modifiedStudent.Id);
                if (student != null)
                {
                    student.FirstName = modifiedStudent.FirstName;
                    student.LastName = modifiedStudent.LastName;
                    student.Email = modifiedStudent.Email;
                    student.Status = modifiedStudent.Status;
                    if (student.SelectionId != modifiedStudent.SelectionId)
                    {
                        var selection = await _context.Selections.FirstOrDefaultAsync(s => s.Id == modifiedStudent.SelectionId);
                        if (selection != null)
                        {
                            student.Selection = selection;
                            student.SelectionId = modifiedStudent.SelectionId;
                        }
                    }
                }
                else
                {
                    throw new Exception("Student with the ID of: " + modifiedStudent.Id + " does not exist.");
                }
                await _context.SaveChangesAsync();
                var fetchedStudent = await _context.Students.Include(s => s.Selection).ThenInclude(s => s.JapProgram).FirstOrDefaultAsync(s => s.Id == modifiedStudent.Id);
                response.Data = _mapper.Map<GetStudentDto>(fetchedStudent);
                response.Message = "You have successfully modified a student " + student.FirstName + " " + student.LastName + ".";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
        public async Task<ServiceResponse<string>> DeleteStudent(int studentId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                response.Message = "You have deleted a student: " + student.FirstName + " " + student.LastName + ".";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public ServiceResponse<GetStudentPageDto> GetStudentsWithParams(int pageNumber, int pageSize, string firstName, string lastName, string email, string selectionName, string japProgramName, StudentStatus? status, string sort, bool descending)
        {
            ServiceResponse<GetStudentPageDto> response = new ServiceResponse<GetStudentPageDto>();
            StudentFetchConfig.Initialize(firstName, lastName, email, status, selectionName, japProgramName, sort, descending);
            try
            {
                var students = _context.Students.Include(s => s.Selection).ThenInclude(s => s.JapProgram).Paginate(pageNumber, pageSize, StudentFetchConfig.sorts, StudentFetchConfig.filters);
                response.Data = _mapper.Map<GetStudentPageDto>(students);
                response.Message = "You have fetched a page no. " + pageNumber + " with " + students.PageSize + " student(s).";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentById(int id)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            try
            {
                var student = await _context.Students
                    .Include(s => s.Comments)
                    .Include(s => s.Selection)
                    .ThenInclude(s => s.JapProgram)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if(student == null)
                {
                    throw new Exception("No student was found.");
                }
                response.Data = _mapper.Map<GetStudentDto>(student);
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCommentDto>>> AddComment(AddCommentDto newComment)
        {
            ServiceResponse<List<GetCommentDto>> response = new ServiceResponse<List<GetCommentDto>>();
            try
            {
                var comment = _mapper.Map<Comment>(newComment);
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                var fetchedComments = await _context.Comments.Where(c => c.StudentId == newComment.StudentId).OrderBy(c => c.CreatedAt).ToListAsync();
                response.Data = fetchedComments.Select(c => _mapper.Map<GetCommentDto>(c)).ToList();
            }
            catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }
    }
}
