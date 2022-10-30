﻿using JAPManagement.Core.DTOs.SelectionDTOs;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.Models.SelectionModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagement.API.Controllers
{
    [Authorize(Roles = "Admin")]
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
            return StatusCode(201, response);
        }

        [HttpGet("get/report")]
        public async Task<ActionResult<ServiceResponse<List<AdminReport>>>> GetSelectionsReport()
        {
            var response = await _selectionService.GetSelectionsReport();
            return Ok(response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetSelectionDto>>>> GetAllSelections()
        {
            ServiceResponse<List<GetSelectionDto>> response = new ServiceResponse<List<GetSelectionDto>>();
            response = await _selectionService.GetAllSelections();
            return Ok(response);
        }

        [HttpGet("get/all/params")]
        public ActionResult<ServiceResponse<GetSelectionPageDto>> GetSelectionsWithParams(string? name, string? japProgramName, DateTime? dateStart, DateTime? dateEnd, SelectionStatus? status, string? sort = "name", int page = 1, int pageSize = 10, bool descending = true)
        {
            ServiceResponse<GetSelectionPageDto> response = new ServiceResponse<GetSelectionPageDto>();
            response = _selectionService.GetSelectionsWithParams(page, pageSize, name, japProgramName, dateStart, dateEnd, status, sort, descending);
            return Ok(response);
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<ServiceResponse<GetSelectionDto>>> GetSelectionById(int selectionId)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            response = await _selectionService.GetSelectionById(selectionId);
            return Ok(response);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ServiceResponse<GetSelectionDto>>> ModifySelection(ModifySelectionDto modifiedSelection)
        {
            ServiceResponse<GetSelectionDto> response = new ServiceResponse<GetSelectionDto>();
            response = await _selectionService.ModifySelection(modifiedSelection);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteSelectionById(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            response = await _selectionService.DeleteSelectionById(id);
            return Ok(response);
        }
    }
}
