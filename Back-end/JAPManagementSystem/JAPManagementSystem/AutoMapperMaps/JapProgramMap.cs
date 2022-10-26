using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.JapItemDTOs;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.DTOs.StudentDTOs;
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
            CreateMap<ModifyItemDto, Program>();
            CreateMap<Page<JapProgram>, GetProgramPageDto>();
            CreateMap<ProgramItemsOrder, ProgramItem>();
            CreateMap<ProgramItem, ProgramItemsOrder>();
            CreateMap<ProgramItem, StudentPersonalProgram>();
        }
    }
}
