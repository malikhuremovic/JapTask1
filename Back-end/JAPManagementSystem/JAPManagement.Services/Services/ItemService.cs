using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Common.FetchConfigs;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.Response;
using JAPManagement.Database.Data;
using JAPManagement.Exceptions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _items;
        private readonly IStudentService _studentService;
        public ItemService(IMapper mapper, IItemRepository items, IStudentService studentService)
        {
            _mapper = mapper;
            _items = items;
            _studentService = studentService;
        }

        public async Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto newItem)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            var item = _mapper.Map<JapItem>(newItem);
            await _items.Add(item);
            response.Data = _mapper.Map<GetItemDto>(item);
            response.Message = "You have successfully added a new " + (item.IsEvent ? "event " : "Item ") + item.Name;
            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> GetItem(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();

            var item = await _items.GetByIdAsync(id);
            if (item == null)
            {
                throw new EntityNotFoundException("Item was not found");
            }
            response.Data = _mapper.Map<GetItemDto>(item);
            response.Message = "You have successfully fetched an item";

            return response;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> GetAllItems()
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();

            var items = await _items.GetAllAsync();
            response.Data = items.Select(Item => _mapper.Map<GetItemDto>(Item)).ToList();
            response.Message = "You have successfully fetched all items";

            return response;
        }
        public async Task<ServiceResponse<GetItemDto>> DeleteItem(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();

            var item = await _items.GetByIdAsync(id);
            if (item == null)
            {
                throw new EntityNotFoundException("Item was not found");
            }
            await _items.Delete(id);
            response.Data = _mapper.Map<GetItemDto>(item);
            response.Message = "You have deleted a Item " + item.Name;

            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> ModifyItem(ModifyItemDto modifiedItem)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();

            var item = await _items.GetByIdAsync(modifiedItem.Id);
            if (item == null)
            {
                throw new EntityNotFoundException("Item was not found");
            }
            await _items.Update(_mapper.Map<JapItem>(modifiedItem));
            response.Data = _mapper.Map<GetItemDto>(item);
            response.Message = "You have successfully modified a Item: " + item.Name + ".";

            return response;
        }

        public ServiceResponse<GetItemPageDto> GetItemsWithParams(int pageNumber, int pageSize, string? name, string? description, string? URL, int? expectedHours, string? isEvent, string sort, bool descending)
        {

            ServiceResponse<GetItemPageDto> response = new ServiceResponse<GetItemPageDto>();
            ItemFetchConfig.Initialize(name, description, URL, expectedHours, isEvent, sort, descending);

            var items = _items.GetItemsWithParams(
                pageNumber,
                pageSize,
                ItemFetchConfig.sorts,
                ItemFetchConfig.filters);
            response.Data = _mapper.Map<GetItemPageDto>(items);
            response.Message = "You have fetched a page no. " + pageNumber + " with " + items.Results.Count() + " Item(s).";

            return response;
        }
    }
}
