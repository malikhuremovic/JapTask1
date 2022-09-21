using JAPManagementSystem.Models;
using JAPManagementSystem.DTOs;

namespace JAPManagementSystem.Services.SelectionService
{
    public interface ISelectionService
    {
        Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection);
        Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections();
        Task<ServiceResponse<GetSelectionDto>> GetSelectionById(int selectionId);
        Task<ServiceResponse<GetSelectionDto>> GetSelectionByName(string selectionName);
        Task<ServiceResponse<List<GetSelectionDto>>> DeleteSelectionByName(string selectionName);
        Task<ServiceResponse<GetSelectionDto>> ModifySelection(ModifySelectionDto modifiedSelection);


    }
}
