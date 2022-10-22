using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.Models.Response;

namespace JAPManagementSystem.Services.ProgramService
{
    public interface IProgramService
    {
        Task<ServiceResponse<GetProgramDto>> AddProgram(AddProgramDto newProgram);
        Task<ServiceResponse<GetProgramDto>> AddProgramItem(AddProgramItemsDto newProgramLectures);
        Task<ServiceResponse<GetProgramDto>> RemoveProgramItem(DeleteProgramItemsDto programLectures);
        Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms();
        Task<ServiceResponse<GetProgramDto>> GetProgramById(int id);

    }
}
