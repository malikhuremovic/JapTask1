using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Models.Response;

namespace JAPManagement.Core.Interfaces
{
    public interface IItemService
    {
        Task<ServiceResponse<GetItemDto>> AddLecture(AddItemDto newLecture);
        Task<ServiceResponse<GetItemDto>> GetLecture(int id);
        Task<ServiceResponse<List<GetItemDto>>> GetAllLectures();
        ServiceResponse<GetItemPageDto> GetLecturesWithParams(int pageNumber, int pageSize, string? name, string? description, string? URL, int? expectedHours, string? isEvent, string sort, bool descending);
        Task<ServiceResponse<GetItemDto>> ModifyLecture(ModifyItemDto modifiedLecture);
        Task<ServiceResponse<GetItemDto>> DeleteLecture(int id);
    }
}
