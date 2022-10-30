using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.DTOs.ProgramDTOs;

namespace JAPManagement.Core.Interfaces.Services
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
