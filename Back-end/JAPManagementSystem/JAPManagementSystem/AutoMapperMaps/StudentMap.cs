using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.Models;

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
        }
    }
}
