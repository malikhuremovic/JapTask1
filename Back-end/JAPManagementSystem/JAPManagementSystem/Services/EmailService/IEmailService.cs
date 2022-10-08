using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.EmailService
{
    public interface IEmailService
    {
        void SendConfirmationEmail(StudentUserCreatedDto student);
    }
}
