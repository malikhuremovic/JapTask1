using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models.Response;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Services.AuthService;
using JAPManagementSystem.Services.EmailService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace JAPManagementSystem.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IEmailService _mailService;

        public StudentService(IAuthService authService, DataContext context, IMapper mapper, IEmailService mailService)
        {
            _authService = authService;
            _context = context;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            try
            {
                StudentUserCreatedDto studentUser = _authService.CreateStudentUser(newStudent);
                Student student = _mapper.Map<Student>(newStudent);
                student = _mapper.Map<Student>(studentUser);
                student.SelectionId = newStudent.SelectionId;
                student.Status = newStudent.Status;
                try
                {
                    var result = await _authService.RegisterStudentUser(student, studentUser.Password);
                    if (!result.Success)
                    {
                        throw new Exception(result.Message);
                    }
                    _mailService.SendConfirmationEmail(studentUser);
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
                await _context.SaveChangesAsync();
                var fetchedStudent = await _context.Students
                    .Include(s => s.Selection)
                    .ThenInclude(s => s.JapProgram)
                    .FirstOrDefaultAsync(s => s.Email
                    .Equals(newStudent.Email));
               
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
                var students = await _context.Students
                    .Include(c => c.Comments)
                    .Include(s => s.Selection)
                    .ThenInclude(s => s.JapProgram)
                    .OrderByDescending(s => s.FirstName)
                    .ToListAsync();
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
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id.Equals(modifiedStudent.Id));
                if (student != null && modifiedStudent != null)
                {
                    student.FirstName = modifiedStudent.FirstName;
                    student.LastName = modifiedStudent.LastName;
                    student.Email = modifiedStudent.Email;
                    student.Status = modifiedStudent.Status;
                    if (student.SelectionId != modifiedStudent.SelectionId)
                    {
                        var selection = await _context.Selections
                            .FirstOrDefaultAsync(s => s.Id == modifiedStudent.SelectionId);
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
                var fetchedStudent = await _context.Students
                    .Include(s => s.Selection).ThenInclude(s => s.JapProgram)
                    .FirstOrDefaultAsync(s => s.Id
                    .Equals(modifiedStudent.Id));
                response.Data = _mapper.Map<GetStudentDto>(fetchedStudent);
                response.Message = "You have successfully modified a student " + student.FirstName + " " + student.LastName + ".";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentById(string id)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            try { 
            var student = await _context.Students
                    .Include(s => s.Comments)
                    .Include(s => s.Selection)
                    .ThenInclude(s => s.JapProgram)
                    .FirstOrDefaultAsync(s => s.Id
                    .Equals(id));

            if (student == null)
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

public async Task<ServiceResponse<GetStudentItemDto>> ModifyStudentItem(string studentId, ModifyStudentItemDto modifiedItem)
        {
            ServiceResponse<GetStudentItemDto> response = new ServiceResponse<GetStudentItemDto>();
            try
            {
                var item = await _context.StudentItems.FirstOrDefaultAsync(st => (st.StudentId == studentId && st.ItemId == modifiedItem.ItemId));
                if(item == null)
                {
                    throw new Exception("There is no item with the ID : " + modifiedItem.ItemId);
                }
                item.Status = modifiedItem.Status;
                item.Done = modifiedItem.Done;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetStudentItemDto>(item);
                response.Message = "You have successfully modified a student program item (lecture, event).";
            }catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteStudent(string studentId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id
                    .Equals(studentId));
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
            StudentFetchConfig.Initialize(
                firstName,
                lastName,
                email,
                status,
                selectionName,
                japProgramName,
                sort,
                descending);
            try
            {
                var students = _context.Students
                    .Include(s => s.Selection)
                    .ThenInclude(s => s.JapProgram)
                    .Paginate(
                    pageNumber,
                    pageSize,
                    StudentFetchConfig.sorts,
                    StudentFetchConfig.filters);
                response.Data = _mapper.Map<GetStudentPageDto>(students);
                response.Message = "You have fetched a page no. " + pageNumber + " with " + students.Results.Count() + " student(s).";
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentByToken(string token)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            try
            {
                var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var id = decodedToken.Claims
                    .Where(claim => claim.Type
                    .Equals("nameid"))
                    .Select(claim => claim.Value)
                    .SingleOrDefault();
                var student = await _context.Students
                    .Include(s => s.Comments)
                    .Include(s => s.Selection)
                    .ThenInclude(s => s.JapProgram)
                    .FirstOrDefaultAsync(s => s.Id
                    .Equals(id));

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
                var fetchedComments = await _context.Comments
                    .Where(c => c.SId
                    .Equals(newComment.SId))
                    .OrderBy(c => c.CreatedAt)
                    .ToListAsync();
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
