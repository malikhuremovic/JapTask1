using AutoMapper;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.Models.ProgramModel;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class JapProgramMap: Profile
    {
        public JapProgramMap()
        {
            CreateMap<AddProgramDto, JapProgram>();
            CreateMap<JapProgram, GetProgramDto>();
            CreateMap<StudentItem, GetStudentItemDto>();
        }
    }
}
