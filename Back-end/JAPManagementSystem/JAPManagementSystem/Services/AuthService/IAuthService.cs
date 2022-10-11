using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.StudentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JAPManagementSystem.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<GetUserDto>> GetUserByToken(string token);
        StudentUserCreatedDto CreateStudentUser(AddStudentDto newStudent);
        Task<ServiceResponse<GetUserDto>> RegisterStudentUser(Student student, string password);
        Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user);
        Task<ServiceResponse<GetUserDto>> RegisterAdminUser(AddAdminDto admin);

    }
}
