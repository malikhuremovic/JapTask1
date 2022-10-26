using AutoMapper;
using DateCalculation;
using EntityFrameworkPaginate;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.StudentDTOs;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models.Response;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Services.AuthService;
using JAPManagementSystem.Services.EmailService;
using JAPManagementSystem.Services.ProgramService;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace JAPManagementSystem.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IEmailService _mailService;
        private readonly IProgramService _programService;
        private readonly DateCalculator _dateCalculation;

        public StudentService(IAuthService authService, DataContext context, IMapper mapper, IEmailService mailService, IProgramService programService)
        {
            _authService = authService;
            _context = context;
            _mapper = mapper;
            _mailService = mailService;
            _programService = programService;
        }

        public List<AddStudentItemDto> CalculateStartAndEndDate(Student student, List<AddStudentItemDto> studentItemList)
        {
            List<AddStudentItemDto> updatedItemList = new List<AddStudentItemDto>();
            for(int i = 0; i < studentItemList.Count; i++)
            {
                var studentItem = studentItemList.ElementAt(i);
                DateTime previousEndDate;
                if(i == 0)
                {
                    previousEndDate = student.Selection.DateStart;
                }
                else
                {
                    previousEndDate = studentItemList.ElementAt(i - 1).EndDate;
                }
                _dateCalculation.CalculateTimeDifference(previousEndDate, studentItem.ExpectedHours, out DateTime newStartDate, out DateTime newEndDate);
                studentItem.StartDate = newStartDate;
                studentItem.EndDate = newEndDate;
                updatedItemList.Add(studentItem);
            }
            return updatedItemList;
        }

        public async Task PopulateStudentItems(string studentId)
        {
            var existingStudentItems = await _context.StudentItems.Where(st => st.StudentId.Equals(studentId)).ToListAsync();
            _context.RemoveRange(existingStudentItems);
            await _context.SaveChangesAsync();
            var student = await _context.Students
                .Where(s => s.Id.Equals(studentId))
                .Include(s => s.Selection)
                .ThenInclude(s => s.JapProgram)
                .FirstAsync();
            var orderedProgramItems = await _context.ProgramItems
                .Where(pt => pt.ProgramId == student.Selection.JapProgramId)
                .OrderBy(pt => pt.Order)
                .Include(pt => pt.Item)
                .ToListAsync();
            var items = orderedProgramItems.Select(pt => pt.Item).ToList(); 
            var studentItemList = new List<AddStudentItemDto>();
            items.ForEach(item =>
            {
                var studentItem = new AddStudentItemDto
                {
                    StudentId = studentId,
                    ItemId = item.Id,
                    Done = 0,
                    Status = StudentItemStatus.NotStarted,
                    ExpectedHours = item.ExpectedHours
                };
                studentItemList.Add(studentItem);
            });

            var updatedItemList = CalculateStartAndEndDate(student, studentItemList);
            List<StudentItem> studentItemsList = updatedItemList.Select(item => _mapper.Map<StudentItem>(item)).ToList();
      
                _context.StudentItems.UpdateRange(studentItemsList);
                _context.StudentItems.AddRange(studentItemsList);
                await _context.SaveChangesAsync();
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
                await PopulateStudentItems(student.Id);
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

        public async Task<ServiceResponse<List<StudentPersonalProgram>>> GetStudentPersonalProgram(string token)
        {
            ServiceResponse<List<StudentPersonalProgram>> response = new ServiceResponse<List<StudentPersonalProgram>>();
            try
            {
                var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var id = decodedToken.Claims
                    .Where(claim => claim.Type
                    .Equals("nameid"))
                    .Select(claim => claim.Value)
                    .SingleOrDefault();
                await PopulateStudentItems(id);
                var student = await _context.Students.Where(s => s.Id.Equals(id)).Include(s => s.Selection).FirstAsync();
                var studentItems = await _context.StudentItems.Where(st => st.StudentId.Equals(id)).Include(s => s.Item).ToListAsync();
                var programItems = await _context.ProgramItems.Where(p => p.ProgramId == student.Selection.JapProgramId).OrderBy(pt => pt.Order).ToListAsync();
                var personalProgram = new List<StudentPersonalProgram>();
                studentItems.ForEach(studentItem =>
                {
                    for(int i = 0; i < programItems.Count; i++)
                    { 
                        if (studentItem.ItemId == programItems[i].ItemId)
                        {
                            personalProgram.Add(new StudentPersonalProgram
                            {
                                Name = studentItem.Item.Name,
                                Description = studentItem.Item.Description,
                                ExpectedHours = studentItem.Item.ExpectedHours,
                                isEvent = studentItem.Item.IsEvent,
                                Done = studentItem.Done,
                                DateStart = studentItem.StartDate,
                                DateEnd = studentItem.EndDate,
                                URL = studentItem.Item.URL,
                                Status = studentItem.Status,
                                Order = programItems[i].Order,
                            });
                            break;
                        }
                    }
                });
                personalProgram.Sort(
                    delegate (StudentPersonalProgram p1, StudentPersonalProgram p2)
                    {
                        return p1.Order.CompareTo(p2.Order);
                    }
                ); 
                response.Data = personalProgram;
                response.Message = "You have successfully fetched personal student program for student: " + student.UserName;
            }catch(Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
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
                await PopulateStudentItems(student.Id);
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
