using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagement.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("/api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> AddItem(AddItemDto newItem)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _itemService.AddItem(newItem);
            return StatusCode(201, response);
        }

        [HttpGet("get")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetItem(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _itemService.GetItem(id);
            return StatusCode(200, response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> GetAllItems()
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();
            response = await _itemService.GetAllItems();
            return StatusCode(200, response);
        }


        [HttpGet("get/all/params")]
        public ActionResult<ServiceResponse<GetItemPageDto>> GetItemWithParams(string? name, string? description, string? URL, int? expectedHours, string? isEvent, string sort = "name", int page = 1, int pageSize = 10, bool descending = true)
        {
            ServiceResponse<GetItemPageDto> response = new ServiceResponse<GetItemPageDto>();
            response = _itemService.GetItemsWithParams(page, pageSize, name, description, URL, expectedHours, isEvent, sort, descending);
            return StatusCode(200, response);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> ModifyItem(ModifyItemDto modifiedItem)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _itemService.ModifyItem(modifiedItem);
            return StatusCode(200, response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> DeleteItem(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            response = await _itemService.DeleteItem(id);
            return StatusCode(200, response);
        }
    }
}
