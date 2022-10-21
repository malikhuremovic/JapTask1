using AutoMapper;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models.ProgramModel;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class JapProgramMap: Profile
    {
        public JapProgramMap()
        {
            CreateMap<AddProgramDto, JapProgram>();
            CreateMap<JapProgram, GetProgramDto>();
        }
    }
}
