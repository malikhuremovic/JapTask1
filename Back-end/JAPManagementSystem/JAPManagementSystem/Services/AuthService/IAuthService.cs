using JAPManagementSystem.Models;

namespace JAPManagementSystem.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
