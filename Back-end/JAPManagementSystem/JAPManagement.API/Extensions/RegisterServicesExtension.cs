using JAPManagenent.Utils.Util;
using JAPManagement.Services.Services.ProgramService;
using JAPManagement.Services.Services.SelectionService;
using JAPManagement.Services.Services.ItemService;
using JAPManagement.Services.Services.HangfireServices;
using JAPManagement.Services.Services.AuthService;
using JAPManagement.Services.Services.StudentService;
using JAPManagement.Services.Services.EmailService;

namespace JAPManagement.API.Extensions
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
