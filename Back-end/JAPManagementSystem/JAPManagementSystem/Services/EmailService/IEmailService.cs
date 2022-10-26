using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.SelectionModel;

namespace JAPManagementSystem.Services.EmailService
{
    public interface IEmailService
    {
        void SendConfirmationEmail(StudentUserCreatedDto student);
        void SendConfirmationEmail(AdminUserCreatedDto admin);
        void SendConfirmationEmail(AdminReport report);

    }
}
