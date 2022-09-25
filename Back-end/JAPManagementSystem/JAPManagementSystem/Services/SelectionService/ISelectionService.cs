using JAPManagementSystem.Models;
using JAPManagementSystem.DTOs.Selection;
using Microsoft.AspNetCore.Mvc;

namespace JAPManagementSystem.Services.SelectionService
{
    public interface ISelectionService
    {
        Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection);
        Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections();
        ServiceResponse<List<GetSelectionDto>> GetSelectionsWithParams(int pageNumber, int pageSize, string? name, int? japProgramId, SelectionStatus? status, int sort, bool descending);
        Task<ServiceResponse<GetSelectionDto>> GetSelectionById(int selectionId);
        Task<ServiceResponse<GetSelectionDto>> ModifySelection(ModifySelectionDto modifiedSelection);
        Task<ServiceResponse<string>> DeleteSelectionById(int id);


    }
}
