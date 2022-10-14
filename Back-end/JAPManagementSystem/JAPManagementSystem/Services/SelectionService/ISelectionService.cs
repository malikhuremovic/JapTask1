using JAPManagementSystem.Models;
using JAPManagementSystem.DTOs.Selection;
using Microsoft.AspNetCore.Mvc;
using JAPManagementSystem.Models.SelectionModel;

namespace JAPManagementSystem.Services.SelectionService
{
    public interface ISelectionService
    {
        Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection);
        Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections();
        Task<ServiceResponse<List<AdminReport>>> GetSelectionsReport();
        ServiceResponse<GetSelectionPageDto> GetSelectionsWithParams(int pageNumber, int pageSize, string? name, string? japProgramName, DateTime? dateStart, DateTime? dateEnd, SelectionStatus? status, string sort, bool descending);
        Task<ServiceResponse<GetSelectionDto>> GetSelectionById(int selectionId);
        Task<ServiceResponse<GetSelectionDto>> ModifySelection(ModifySelectionDto modifiedSelection);
        Task<ServiceResponse<string>> DeleteSelectionById(int id);


    }
}
