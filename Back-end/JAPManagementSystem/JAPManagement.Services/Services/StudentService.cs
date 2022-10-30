using AutoMapper;
using JAPManagement.Common;
using JAPManagement.Core.DTOs.Comment;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.Models.StudentModel;
using JAPManagement.Database.Data;
using JAPManagement.ExceptionHandler.Exceptions;
using JAPManagenent.Utils.Util;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace JAPManagement.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IEmailService _mailService;
        private readonly IProgramService _programService;
        private readonly IDateCalculator _dateCalculation;
        private readonly IStudentRepository _students;
        private readonly ISelectionRepository _selections;
        private readonly IProgramItemRepository _programItems;
        private readonly IStudentItemRepository _studentItems;

        public StudentService(IProgramItemRepository programItem, ISelectionRepository selections, IAuthService authService, IStudentRepository students, IMapper mapper, IEmailService mailService, IProgramService programService, IDateCalculator dateCalculator, IStudentItemRepository studentItems)
        {
            _programItems = programItem;
            _authService = authService;
            _mapper = mapper;
            _mailService = mailService;
            _programService = programService;
            _dateCalculation = dateCalculator;
            _students = students;
            _selections = selections;
            _studentItems = studentItems;
        }

        public async Task PopulateStudentItems(string studentId)
        {
            var existingStudentItems = await _studentItems.GetByStudentIdAsync(studentId);
            await _studentItems.DeleteRange(existingStudentItems);
            var student = await _students.GetByIdWithSelectionAndProgram(studentId);
            var orderedProgramItems = await _programItems.GetProgramItemsAsync(student.Selection.JapProgramId);
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
            
            var updatedItemList = _dateCalculation.CalculateStartAndEndDate(student, studentItemList);
            List<StudentItem> studentItemsList = updatedItemList.Select(item => _mapper.Map<StudentItem>(item)).ToList();

            await _studentItems.AddRange(studentItemsList);
        }

        public async Task<ServiceResponse<GetStudentDto>> AddStudent(AddStudentDto newStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();

            StudentUserCreatedDto studentUser = _authService.CreateStudentUser(newStudent);
            Student student = _mapper.Map<Student>(newStudent);
            student = _mapper.Map<Student>(studentUser);
            student.SelectionId = newStudent.SelectionId;
            student.Status = newStudent.Status;

            await _authService.RegisterStudentUser(student, studentUser.Password);
            _mailService.SendConfirmationEmail(studentUser);

            await PopulateStudentItems(student.Id);
            var fetchedStudent = await _students.GetByIdWithSelectionAndProgram(student.Id);

            response.Data = _mapper.Map<GetStudentDto>(fetchedStudent);
            response.Message = "You have successfully added a new student";


            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents()
        {
            ServiceResponse<List<GetStudentDto>> response = new ServiceResponse<List<GetStudentDto>>();

            var students = await _students.GetAllWithSelectionAndProgram();
            response.Data = students.Select(s => _mapper.Map<GetStudentDto>(s)).ToList();
            response.Message = "You have fetched all the students in the database.";

            return response;
        }

        public async Task<ServiceResponse<List<StudentPersonalProgram>>> GetStudentPersonalProgram(string token)
        {
            ServiceResponse<List<StudentPersonalProgram>> response = new ServiceResponse<List<StudentPersonalProgram>>();

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();
            await PopulateStudentItems(id);
            var student = await _students.GetByIdWithSelectionAndProgram(id);
            var studentItems = await _studentItems.GetByStudentIdAsync(id);
            var programItems = await _programItems.GetProgramItemsAsync(student.Selection.JapProgramId);

            response.Data = CreatePersonalProgram(studentItems, programItems);
            response.Message = "You have successfully fetched personal student program for student: " + student.UserName;

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> ModifyStudent(ModifyStudentDto modifiedStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();

            var student = await _students.GetByIdAsync(modifiedStudent.Id);
            if (student == null)
            {
                throw new EntityNotFoundException("Student was not found");
            }
            var selection = await _selections.GetByIdWithProgramAsync(modifiedStudent.SelectionId);
            if (selection == null)
            {
                throw new EntityNotFoundException("Selection was not found");
            }
            var updatedStudent = await _students.Update(_mapper.Map<Student>(modifiedStudent));
            updatedStudent.Selection = selection;
            await PopulateStudentItems(updatedStudent.Id);
            response.Data = _mapper.Map<GetStudentDto>(updatedStudent);
            response.Message = "You have successfully modified a student " + student.FirstName + " " + student.LastName + ".";

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentById(string id)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();

            var student = await _students.GetByIdWithCommentAndSelectionAndProgram(id);

            if (student == null)
            {
                throw new EntityNotFoundException("Student was not found");
            }
            response.Data = _mapper.Map<GetStudentDto>(student);

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteStudent(string studentId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var student = await _students.Delete(studentId);
            response.Message = "You have deleted a student: " + student.FirstName + " " + student.LastName + ".";

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

            var students = _students.GetStudentsWithParams(pageNumber, pageSize, StudentFetchConfig.sorts, StudentFetchConfig.filters);
            response.Data = _mapper.Map<GetStudentPageDto>(students);
            response.Message = "You have fetched a page no. " + pageNumber + " with " + students.Results.Count() + " student(s).";

            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentByToken(string token)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();
            var student = await _students.GetByIdWithCommentAndSelectionAndProgram(id);

            if (student == null)
            {
                throw new EntityNotFoundException("Student was not found");
            }
            response.Data = _mapper.Map<GetStudentDto>(student);

            return response;
        }

        public async Task<ServiceResponse<List<GetCommentDto>>> AddComment(AddCommentDto newComment)
        {
            ServiceResponse<List<GetCommentDto>> response = new ServiceResponse<List<GetCommentDto>>();

            var fetchedComments = await _students.AddStudentComment(_mapper.Map<Comment>(newComment));
            response.Data = fetchedComments.Select(c => _mapper.Map<GetCommentDto>(c)).ToList();

            return response;
        }

        private List<StudentPersonalProgram> CreatePersonalProgram(List<StudentItem> studentItems, List<ProgramItem> programItems) {
            var personalProgram = new List<StudentPersonalProgram>();
            studentItems.ForEach(studentItem =>
            {
                for (int i = 0; i < programItems.Count; i++)
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
                    delegate (StudentPersonalProgram sp1, StudentPersonalProgram sp2)
                    {
                        return sp1.Order.CompareTo(sp2.Order);
                    }
                );
            return personalProgram;
        }
    }
}
