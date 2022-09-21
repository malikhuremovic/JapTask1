using JAPManagementSystem.DTOs;
using JAPManagementSystem.Models;
using JAPManagementSystem.Services.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagementSystem.Controllers
{
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

        [HttpGet("get/name/{studentName}")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> GetStudentByName(string studentName)
        {
            ServiceResponse<List<GetStudentDto>> response = new ServiceResponse<List<GetStudentDto>>();
            response = await _studentService.GetStudentByName(studentName);
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


    }
}
