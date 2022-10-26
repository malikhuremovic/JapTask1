using JAPManagementSystem.Services.AuthService;
using JAPManagementSystem.Services.EmailService;
using JAPManagementSystem.Services.LectureService;
using JAPManagementSystem.Services.ProgramService;
using JAPManagementSystem.Services.SelectionService;
using JAPManagementSystem.Services.StudentService;
using Hangfire;
using Hangfire.MemoryStorage;
using JAPManagementSystem.Services.HangfireServices;
using DateCalculation.Util;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddTransient<ISelectionService, SelectionService>();
            service.AddTransient<IProgramService, ProgramService>();
            service.AddTransient<IStudentService, StudentService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IItemService, ItemService>();
            service.AddScoped<IHangfireReportService, HangfireReportService>();
            service.AddScoped<IDateCalculator, DateCalculator>();
            service.AddSingleton<IEmailService, EmailService>();
        }
    }
}
