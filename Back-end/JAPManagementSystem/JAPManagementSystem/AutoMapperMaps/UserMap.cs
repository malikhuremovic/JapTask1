using AutoMapper;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.AutoMapperMaps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<StudentUserCreatedDto, Student>();
            CreateMap<User, GetUserDto>();
        }
    }
}
