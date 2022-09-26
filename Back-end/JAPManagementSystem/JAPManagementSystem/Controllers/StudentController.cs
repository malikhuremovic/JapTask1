using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.Student;
using JAPManagementSystem.Models;
using JAPManagementSystem.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JAPManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> AddStudent(AddStudentDto newStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            response = await _studentService.AddStudent(newStudent);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> GetAllSudents()
        {
            ServiceResponse<List<GetStudentDto>> response = new ServiceResponse<List<GetStudentDto>>();
            response = await _studentService.GetAllStudents();
            if (!response.Success)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> GetStudentById(int id)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            response = await _studentService.GetStudentById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("get")]
        public ActionResult<ServiceResponse<GetStudentPageDto>> GetStudentsWithParams(string? firstName, string? lastName, string? email, string? selectionName, string? japProgramName, StudentStatus? status, int page = 1, int pageSize = 10, string? sort = "firstName", bool descending = true)
        {
            ServiceResponse<GetStudentPageDto> response = new ServiceResponse<GetStudentPageDto>();
            response = _studentService.GetStudentsWithParams(page, pageSize, firstName, lastName, email, selectionName, japProgramName, status, sort, descending);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> GetStudentByName(ModifyStudentDto modifiedStudent)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            response = await _studentService.ModifyStudent(modifiedStudent);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteStudentById(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            response = await _studentService.DeleteStudent(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("add/comment")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> AddComment(AddCommentDto newComment)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            response = await _studentService.AddComment(newComment);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
