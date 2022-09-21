using AutoMapper;
using JAPManagementSystem.DTOs;
using JAPManagementSystem.Models;

namespace JAPManagementSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddSelectionDto, Selection>();
            CreateMap<Selection, GetSelectionDto>();
            CreateMap<AddProgramDto, JapProgram>();
            CreateMap<JapProgram, GetProgramDto>();
            CreateMap<AddStudentDto, Student>();
            CreateMap<Student, GetStudentDto>();
            CreateMap<ModifySelectionDto, Selection>().ForMember(dest => dest.Students, act => act.Ignore());
            CreateMap<JapProgram, GetJapProgramDto>();
            CreateMap<AddJapProgramDto, JapProgram>();
            CreateMap<ModifyStudentDto, Student>();
        }
    }
}
