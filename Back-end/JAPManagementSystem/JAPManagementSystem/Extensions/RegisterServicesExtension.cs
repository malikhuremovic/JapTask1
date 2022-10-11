using JAPManagementSystem.Services.AuthService;
using JAPManagementSystem.Services.EmailService;
using JAPManagementSystem.Services.ProgramService;
using JAPManagementSystem.Services.SelectionService;
using JAPManagementSystem.Services.StudentService;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection service)
        {
            service.AddScoped<ISelectionService, SelectionService>();
            service.AddScoped<IProgramService, ProgramService>();
            service.AddScoped<IStudentService, StudentService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddSingleton<IEmailService, EmailService>();
        }
    }
}
