using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.LectureService
{
    public interface IItemService
    {
        Task<ServiceResponse<GetItemDto>> AddLecture(AddItemDto newLecture);
        Task<ServiceResponse<GetItemDto>> GetLecture(int id);
        ServiceResponse<GetItemPageDto> GetLecturesWithParams(int pageNumber, int pageSize, string? name, string? description, string? URL, int? expectedHours, string sort, bool descending);
        Task<ServiceResponse<GetItemDto>> DeleteLecture(int id);
        Task<ServiceResponse<GetItemDto>> ModifyLecture(ModifyItemDto modifiedLecture);
    }
}
