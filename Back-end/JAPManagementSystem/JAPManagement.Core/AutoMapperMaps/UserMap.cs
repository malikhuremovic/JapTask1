using AutoMapper;
using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Models.StudentModel;
using JAPManagement.Core.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace JAPManagement.Core.AutoMapperMaps
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
