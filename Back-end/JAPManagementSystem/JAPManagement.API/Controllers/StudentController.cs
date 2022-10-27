using JAPManagement.Core.DTOs.Comment;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.Models.StudentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("add/comment")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> AddComment(AddCommentDto newComment)
        {
            ServiceResponse<List<GetCommentDto>> response = new ServiceResponse<List<GetCommentDto>>();
            response = await _studentService.AddComment(newComment);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin,Student")]
        [HttpGet("get/report")]
        public async Task<ActionResult<ServiceResponse<List<StudentPersonalProgram>>>> GetStudentPersonalProgram()
        {
            ServiceResponse<List<StudentPersonalProgram>> response = new ServiceResponse<List<StudentPersonalProgram>>();
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            response = await _studentService.GetStudentPersonalProgram(tokenValue);
            if (!response.Success)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }


        [Authorize(Roles = "Admin, Student")]
        [HttpGet("get/id")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> GetStudentByToken()
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            response = await _studentService.GetStudentByToken(tokenValue);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/id/admin")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> GetStudentById(string id)
        {
            ServiceResponse<GetStudentDto> response = new ServiceResponse<GetStudentDto>();
            response = await _studentService.GetStudentById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/all/params")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin, Student")]
        [HttpPatch("/item/modify")]
        public async Task<ActionResult<ServiceResponse<GetStudentItemDto>>> ModifyStudentItem(ModifyStudentItemDto modifiedItem)
        {
            ServiceResponse<GetStudentItemDto> response = new ServiceResponse<GetStudentItemDto>();
            Request.Headers.TryGetValue("Authorization", out var token);
            string tokenValue = token
                .ToString()
                .Split(" ")
                .ElementAt(1);
            response = await _studentService.ModifyStudentItem(tokenValue, modifiedItem);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteStudentById(string id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            response = await _studentService.DeleteStudent(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
