using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.EmailService
{
    public interface IEmailService
    {
        void SendConfirmationEmail(AddStudentDto student);
    }
}
