using AutoMapper;
using EntityFrameworkPaginate;
using JAPManagementSystem.DTOs.StudentDto;
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
        }
    }
}
