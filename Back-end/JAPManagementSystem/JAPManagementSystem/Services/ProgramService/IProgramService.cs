using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.Models.Response;
using JAPManagementSystem.Models.ProgramModel;

namespace JAPManagementSystem.Services.ProgramService
{
    public interface IProgramService
    {
        Task<ServiceResponse<GetProgramDto>> AddProgram(AddProgramDto newProgram);
        Task<ServiceResponse<GetProgramDto>> AddProgramItem(AddProgramItemsDto newProgramLectures);
        ServiceResponse<GetProgramPageDto> GetProgramsWithParams(int pageNumber, int pageSize, string? name, string? content, string sort, bool descending);
        Task<ServiceResponse<List<GetItemDto>>> GetProgramItems(int id);
        Task<ServiceResponse<GetProgramDto>> GetProgramById(int id);
        Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms();
        Task<ServiceResponse<GetProgramDto>> ModifyProgram(ModifyProgramDto modifiedProgram);
        Task<ServiceResponse<List<ProgramItem>>> ModifyProgramItemsOrder(AddProgramItemsOrder programItemsOrder);
        Task<ServiceResponse<GetProgramDto>> DeleteProgram(int id);
        Task<ServiceResponse<GetProgramDto>> RemoveProgramItem(DeleteProgramItemsDto programLectures);

    }
}
