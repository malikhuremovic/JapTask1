using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.AuthService
{
    public interface IAuthService
    {
        StudentUserCreatedDto CreateUser(AddStudentDto newStudent);
        Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user);
    }
}
