using JAPManagementSystem.Models;
using JAPManagementSystem.DTOs;

namespace JAPManagementSystem.Services.ProgramService
{
    public interface IProgramService
    {
        Task<ServiceResponse<GetProgramDto>> AddProgram(AddProgramDto newProgram);
        Task<ServiceResponse<List<GetProgramDto>>> GetAllPrograms();
    }
}
