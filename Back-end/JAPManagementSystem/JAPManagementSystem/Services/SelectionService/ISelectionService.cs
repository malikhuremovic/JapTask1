using JAPManagementSystem.Models;
using JAPManagementSystem.DTOs.Selection;

namespace JAPManagementSystem.Services.SelectionService
{
    public interface ISelectionService
    {
        Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection);
        Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections();
        ServiceResponse<List<GetSelectionDto>> GetSelectionsWithParams(int pageNumber, int pageSize, string? name, int japProgramId, int sort);
        Task<ServiceResponse<GetSelectionDto>> GetSelectionById(int selectionId);
        Task<ServiceResponse<List<GetSelectionDto>>> DeleteSelectionByName(string selectionName);
        Task<ServiceResponse<GetSelectionDto>> ModifySelection(ModifySelectionDto modifiedSelection);


    }
}
