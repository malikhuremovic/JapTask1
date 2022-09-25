using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Student;
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
                response.Data = _mapper.Map<GetStudentDto>(student);
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
                var students = await _context.Students.Include("Selection").OrderByDescending(s => s.FirstName).ToListAsync();
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
            Filters<Student> filters = new Filters<Student>();
            filters.Add(!string.IsNullOrEmpty(firstName), s => s.FirstName.Contains(firstName));
            filters.Add(!string.IsNullOrEmpty(lastName), s => s.FirstName.Contains(lastName));
            filters.Add(!string.IsNullOrEmpty(email), s => s.Email.Contains(email));
            filters.Add(status.HasValue, s => s.Status.Equals(status));
            filters.Add(!string.IsNullOrEmpty(selectionName), s => s.Selection.Name.Contains(selectionName));
            filters.Add(!string.IsNullOrEmpty(japProgramName), s => s.Selection.JapProgram.Name.Contains(japProgramName));
            

            Sorts<Student> sorts = new Sorts<Student>();
            sorts.Add(sort.Equals("firstName"), s => s.FirstName, descending);
            sorts.Add(sort.Equals("lastName"), s => s.LastName, descending);
            sorts.Add(sort.Equals("email"), s => s.Email, descending);
            sorts.Add(sort.Equals("selection"), s => s.Selection.Name, descending);
            sorts.Add(sort.Equals("program"), s => s.Selection.JapProgram.Name, descending);

            try
            {
                var students = _context.Students.Include(s => s.Selection).ThenInclude(s => s.JapProgram).Paginate(pageNumber, pageSize, sorts, filters);
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
    }
}
