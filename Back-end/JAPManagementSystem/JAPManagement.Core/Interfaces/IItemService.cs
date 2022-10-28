using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Models.Response;

namespace JAPManagement.Core.Interfaces
{
    public interface IItemService
    {
        Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto newItem);
        Task<ServiceResponse<GetItemDto>> GetItem(int id);
        Task<ServiceResponse<List<GetItemDto>>> GetAllItems();
        ServiceResponse<GetItemPageDto> GetItemsWithParams(int pageNumber, int pageSize, string? name, string? description, string? URL, int? expectedHours, string? isEvent, string sort, bool descending);
        Task<ServiceResponse<GetItemDto>> ModifyItem(ModifyItemDto modifiedItem);
        Task<ServiceResponse<GetItemDto>> DeleteItem(int id);
    }
}
