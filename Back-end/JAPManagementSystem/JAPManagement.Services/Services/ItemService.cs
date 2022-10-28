using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Common;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.Response;
using JAPManagement.Database.Data;
using JAPManagement.ExceptionHandler.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IStudentService _studentService;
        public ItemService(IMapper mapper, DataContext context, IStudentService studentService)
        {
            _mapper = mapper;
            _context = context;
            _studentService = studentService;
        }

        public async Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto newItem)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();
            var Item = _mapper.Map<JapItem>(newItem);
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetItemDto>(Item);
            response.Message = "You have successfully added a new " + (newItem.IsEvent ? "event " : "Item ") + newItem.Name;
            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> GetItem(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();

            var Item = await _context.Items.FirstOrDefaultAsync(l => l.Id == id);
            if (Item == null)
            {
                throw new EntityNotFoundException("Item was not found");
            }
            response.Data = _mapper.Map<GetItemDto>(Item);
            response.Message = "You have successfully fetched a Item";

            return response;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> GetAllItems()
        {
            ServiceResponse<List<GetItemDto>> response = new ServiceResponse<List<GetItemDto>>();

            var Items = await _context.Items.ToListAsync();
            response.Data = Items.Select(Item => _mapper.Map<GetItemDto>(Item)).ToList();
            response.Message = "You have successfully fetched all items";

            return response;
        }


        public async Task<ServiceResponse<GetItemDto>> DeleteItem(int id)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();

            var Item = await _context.Items.FirstOrDefaultAsync(l => l.Id == id);
            if (Item == null)
            {
                throw new EntityNotFoundException("Item was not found");
            }
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetItemDto>(Item);
            response.Message = "You have deleted a Item " + Item.Name;

            return response;
        }

        public async Task<ServiceResponse<GetItemDto>> ModifyItem(ModifyItemDto modifiedItem)
        {
            ServiceResponse<GetItemDto> response = new ServiceResponse<GetItemDto>();

            var Item = await _context.Items
                .FirstOrDefaultAsync(l => l.Id == modifiedItem.Id);
            if (Item == null)
            {
                throw new EntityNotFoundException("Item was not found");
            }
            Item.Name = modifiedItem.Name;
            Item.URL = modifiedItem.URL;
            Item.Description = modifiedItem.Description;
            Item.ExpectedHours = modifiedItem.ExpectedHours;
            Item.IsEvent = modifiedItem.IsEvent;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetItemDto>(Item);
            response.Message = "You have successfully modified a Item: " + Item.Name + ".";

            return response;
        }

        public ServiceResponse<GetItemPageDto> GetItemsWithParams(int pageNumber, int pageSize, string? name, string? description, string? URL, int? expectedHours, string? isEvent, string sort, bool descending)
        {

            ServiceResponse<GetItemPageDto> response = new ServiceResponse<GetItemPageDto>();
            ItemFetchConfig.Initialize(name, description, URL, expectedHours, isEvent, sort, descending);

            var Items = _context.Items
                .Paginate(
                pageNumber,
                pageSize,
                ItemFetchConfig.sorts,
                ItemFetchConfig.filters);
            response.Data = _mapper.Map<GetItemPageDto>(Items);
            response.Message = "You have fetched a page no. " + pageNumber + " with " + Items.Results.Count() + " Item(s).";

            return response;
        }
    }
}
