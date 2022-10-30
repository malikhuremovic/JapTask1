using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.Models.StudentModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JAPManagement.Core.Interfaces.Services
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
