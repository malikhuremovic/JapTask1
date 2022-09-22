using AutoMapper;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.DTOs.Student;
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
            CreateMap<ModifyStudentDto, Student>();
        }
    }
}
