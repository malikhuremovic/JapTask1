using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.Models.ProgramModel;

namespace JAPManagement.Core.Interfaces.Repositories
{
    public interface IProgramItemRepository
    {
        Task<List<ProgramItem>> GetProgramItemsAsync(int programId);
        Task ModifyProgramItemsOrderAsync(List<ProgramItem> programItems);
    }
}
