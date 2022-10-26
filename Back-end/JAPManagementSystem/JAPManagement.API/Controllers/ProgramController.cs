using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.Response;
using JAPManagement.Services.Services.ProgramService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JAPManagement.API.Controllers
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

        [HttpGet("get/all/params")]
        public ActionResult<ServiceResponse<GetProgramPageDto>> GetProgramsWithParams(string? name, string? content, string sort = "name", int page = 1, int pageSize = 10, bool descending = true)
        {
            ServiceResponse<GetProgramPageDto> response = new ServiceResponse<GetProgramPageDto>();
            response = _programService.GetProgramsWithParams(page, pageSize, name, content, sort, descending);
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
                return StatusCode(500, response);
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

        [HttpGet("get/items")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> GetProgramItems(int id)
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();
            response = await _programService.GetProgramItems(id);
            if (response.Success == false)
            {
                return StatusCode(500, response);
            }
            return StatusCode(201, response);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ServiceResponse<GetProgramDto>>> ModifyProgram(ModifyProgramDto modifiedProgram)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            response = await _programService.ModifyProgram(modifiedProgram);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpPatch("modify/items/order")]
        public async Task<ActionResult<ServiceResponse<List<ProgramItem>>>> ModifyProgramItemsOrder(AddProgramItemsOrder programItemsOrder)
        {
            ServiceResponse<List<ProgramItem>> response = new ServiceResponse<List<ProgramItem>>();
            response = await _programService.ModifyProgramItemsOrder(programItemsOrder);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<GetProgramDto>>> DeleteProgram(int id)
        {
            ServiceResponse<GetProgramDto> response = new ServiceResponse<GetProgramDto>();
            response = await _programService.DeleteProgram(id);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
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
