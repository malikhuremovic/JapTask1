using JAPManagementSystem.DTOs;
using JAPManagementSystem.Models;
using JAPManagementSystem.Services.SelectionService;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SelectionController : ControllerBase
    {
        private readonly ISelectionService _selectionService;
        public SelectionController(ISelectionService selectionService)
        {
            _selectionService = selectionService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetSelectionDto>>> AddSelection(AddSelectionDto newSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            response = await _selectionService.AddSelection(newSelection);
            if(response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetSelectionDto>>>> GetAllSelections()
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            response = await _selectionService.GetAllSelections();
            if(response.Success == false)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpGet("get/id/{selectionId}")]
        public async Task<ActionResult<ServiceResponse<GetSelectionDto>>> GetSelectionById(int selectionId)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            response = await _selectionService.GetSelectionById(selectionId);
            if (response.Success == false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("get/name/{selectionName}")]
        public async Task<ActionResult<ServiceResponse<GetSelectionDto>>> GetSelectionByName(string selectionName)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            response = await _selectionService.GetSelectionByName(selectionName);
            if (response.Success == false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/name/{selectionName}")]
        public async Task<ActionResult<ServiceResponse<List<GetSelectionDto>>>> DeleteSelectionByName(string selectionName)
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            response = await _selectionService.DeleteSelectionByName(selectionName);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ServiceResponse<GetSelectionDto>>> ModifySelection(ModifySelectionDto modifiedSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            response = await _selectionService.ModifySelection(modifiedSelection);
            if(response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
