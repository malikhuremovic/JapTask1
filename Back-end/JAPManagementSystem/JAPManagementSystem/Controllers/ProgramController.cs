using JAPManagementSystem.DTOs;
using JAPManagementSystem.Models;
using JAPManagementSystem.Services.ProgramService;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagementSystem.Controllers
{
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

        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<GetProgramDto>>>> GetAllPrograms()
        {
            ServiceResponse<List<GetProgramDto>> response = new ServiceResponse<List<GetProgramDto>>();
            response = await _programService.GetAllPrograms();
            if (response.Success == false)
            {
                return StatusCode(500,response);
            }
            return StatusCode(201, response);
        }

    }
}
