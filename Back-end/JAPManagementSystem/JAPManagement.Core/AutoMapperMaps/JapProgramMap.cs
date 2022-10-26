using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.JapItemDTOs;
using JAPManagement.Core.DTOs.Program;
using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.AutoMapperMaps
{
    public class JapProgramMap : Profile
    {
        public JapProgramMap()
        {
            CreateMap<AddProgramDto, JapProgram>();
            CreateMap<JapProgram, GetProgramDto>();
            CreateMap<StudentItem, GetStudentItemDto>();
            CreateMap<ModifyItemDto, JapProgram>();
            CreateMap<Page<JapProgram>, GetProgramPageDto>();
            CreateMap<ProgramItemsOrder, ProgramItem>();
            CreateMap<ProgramItem, ProgramItemsOrder>();
            CreateMap<ProgramItem, StudentPersonalProgram>();
        }
    }
}
