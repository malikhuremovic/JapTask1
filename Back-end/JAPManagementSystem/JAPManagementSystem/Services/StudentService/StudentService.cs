using AutoMapper;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs;
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
                var students = await _context.Students.Include("Selection").OrderByDescending(s => s.Name).ToListAsync();
                response.Data = students.Select(s => _mapper.Map<GetStudentDto>(s)).ToList();
                response.Message = "You have fetched all the students in the database.";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> GetStudentByName(string studentName)
        {
            ServiceResponse<List<GetStudentDto>> response = new ServiceResponse<List<GetStudentDto>>();
            try
            {
                var students = await _context.Students.Include("Selection").OrderByDescending(s => s.Name).Where(s => s.Name.Contains(studentName)).ToListAsync();
                response.Data = students.Select(s => _mapper.Map<GetStudentDto>(s)).ToList();
                response.Message = "You have fetched all the students in the database.";
            }
            catch (Exception exc)
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
                student.Name = modifiedStudent.Name;
                student.Email = modifiedStudent.Email;
                student.Status = modifiedStudent.Status;
                if(student.SelectionId != modifiedStudent.SelectionId)
                {
                    var selection = await _context.Selections.FirstOrDefaultAsync(s => s.Id == modifiedStudent.SelectionId);
                    if(selection != null)
                    {
                        student.Selection = selection;
                        student.SelectionId = modifiedStudent.SelectionId;
                    }
                }
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetStudentDto>(student);
                response.Message = "You have successfully modified a student " + student.Name + ".";
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
                response.Message = "You have deleted a student: " + student.Name + ".";
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
