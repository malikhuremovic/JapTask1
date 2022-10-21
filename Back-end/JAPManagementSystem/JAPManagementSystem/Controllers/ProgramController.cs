using AutoMapper.Configuration.Conventions;
using JAPManagementSystem.DTOs.LectureDTOs;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models;
using JAPManagementSystem.Services.ProgramService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("/api/[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProgramDto>>> AddProgram(AddProgramDto newProgram)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            response = await _programService.AddProgram(newProgram);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }
        [HttpPost("add/items")]
        public async Task<ActionResult<ServiceResponse<GetProgramDto>>> AddProgramLecture(AddProgramItemsDto newProgramLectures)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            response = await _programService.AddProgramItem(newProgramLectures);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetProgramDto>>>> GetAllProgram()
        {
            ServiceResponse<List<GetProgramDto>> response = new ServiceResponse<List<GetProgramDto>>();
            response = await _programService.GetAllPrograms();
            if (response.Success == false)
            {
                return StatusCode(500,response);
            }
            return StatusCode(201, response);
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<ServiceResponse<GetProgramDto>>> GetProgramById(int id)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            response = await _programService.GetProgramById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("remove/items")]
        public async Task<ActionResult<ServiceResponse<GetProgramDto>>> RemoveProgramLectures(DeleteProgramItemsDto programLectures)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            response = await _programService.RemoveProgramItem(programLectures);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }
    }
}
