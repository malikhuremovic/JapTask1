using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.StudentDTOs;
using JAPManagementSystem.Models.StudentModel;

namespace JAPManagementSystem.AutoMapperMaps
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
            CreateMap<StudentItem, StudentPersonalProgram>();
        }
    }
}
