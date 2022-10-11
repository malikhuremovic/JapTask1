using AutoMapper;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<StudentUserCreatedDto, Student>();
            CreateMap<User, GetUserDto>();
            CreateMap<IdentityUser, User>();
            CreateMap<User, Student>();
            CreateMap<User, Admin>();
            CreateMap<AddAdminDto, Admin>();
            CreateMap<Admin, GetUserDto>();
            CreateMap<Student, GetUserDto>();
            CreateMap<Admin, AdminUserCreatedDto>();
        }
    }
}
