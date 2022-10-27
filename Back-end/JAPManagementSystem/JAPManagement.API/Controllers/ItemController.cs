﻿using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _lectureService;
        public ItemController(IItemService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> AddLecture(AddItemDto newLecture)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _lectureService.AddLecture(newLecture);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpGet("get")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetLecture(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _lectureService.GetLecture(id);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> GetAllLectures()
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();
            response = await _lectureService.GetAllLectures();
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }


        [HttpGet("get/all/params")]
        public ActionResult<ServiceResponse<GetItemPageDto>> GetLectureWithParams(string? name, string? description, string? URL, int? expectedHours, string? isEvent, string sort = "name", int page = 1, int pageSize = 10, bool descending = true)
        {
            ServiceResponse<GetItemPageDto> response = new ServiceResponse<GetItemPageDto>();
            response = _lectureService.GetLecturesWithParams(page, pageSize, name, description, URL, expectedHours, isEvent, sort, descending);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> ModifyLecture(ModifyItemDto modifiedLecture)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _lectureService.ModifyLecture(modifiedLecture);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> DeleteLecture(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _lectureService.DeleteLecture(id);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return StatusCode(201, response);
        }
    }
}
