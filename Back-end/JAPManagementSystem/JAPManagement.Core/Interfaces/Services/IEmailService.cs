using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Models.SelectionModel;

namespace JAPManagement.Core.Interfaces.Services
{
    public interface IEmailService
    {
        void SendConfirmationEmail(StudentUserCreatedDto student);
        void SendConfirmationEmail(AdminUserCreatedDto admin);
        void SendConfirmationEmail(AdminReport report);

    }
}
