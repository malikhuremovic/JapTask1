using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models.Response;
using JAPManagementSystem.Models.StudentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JAPManagementSystem.Services.AuthService
{
    public interface IAuthService
    {
        StudentUserCreatedDto CreateStudentUser(AddStudentDto newStudent);
        Task<ServiceResponse<GetUserDto>> RegisterStudentUser(Student student, string password);
        Task<ServiceResponse<GetUserDto>> RegisterAdminUser(AddAdminDto admin);
        Task<ServiceResponse<GetUserDto>> GetUserByToken(string token);
        Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user);
    }
}
