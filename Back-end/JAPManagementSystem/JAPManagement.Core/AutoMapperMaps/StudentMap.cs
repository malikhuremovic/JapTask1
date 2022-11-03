using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.Models.SelectionModel;
using JAPManagement.Core.Models.StudentModel;

namespace JAPManagement.Core.AutoMapperMaps
{
    public class StudentMap : Profile
    {
        public StudentMap()
        {
            CreateMap<AddStudentDto, Student>();
            CreateMap<Student, GetStudentDto>();
            CreateMap<ModifyStudentDto, Student>();
            CreateMap<Page<Student>, GetStudentPageDto>();
            CreateMap<AddStudentItemDto, StudentItem>();
            CreateMap<StudentItem, StudentItemDto>();
            CreateMap<Selection, Student>();
            CreateMap<StudentItem, StudentPersonalProgram>();
        }
    }
}
