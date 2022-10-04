using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.Comment;
using JAPManagementSystem.DTOs.Program;
using JAPManagementSystem.DTOs.Selection;
using JAPManagementSystem.DTOs.StudentDto;
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
            CreateMap<Page<Student>, GetStudentPageDto>();
            CreateMap<Page<Selection>, GetSelectionPageDto>();
            CreateMap<Comment, GetCommentDto>();
            CreateMap<AddCommentDto, Comment>();
        }
    }
}
