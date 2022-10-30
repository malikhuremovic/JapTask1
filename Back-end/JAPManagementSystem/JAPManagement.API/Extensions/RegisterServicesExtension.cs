using JAPManagenent.Utils.Util;
using JAPManagement.Services.Services.HangfireServices;
using JAPManagement.Services.Services;
using JAPManagement.Core.Interfaces.Services;

namespace JAPManagement.API.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddTransient<ISelectionService, SelectionService>();
            service.AddTransient<IProgramService, ProgramService>();
            service.AddTransient<IStudentService, StudentService>();
            service.AddTransient<IAuthService, AuthService>();
            service.AddTransient<IItemService, ItemService>();
            service.AddTransient<IHangfireReportService, HangfireReportService>();
            service.AddTransient<IDateCalculator, DateCalculator>();
            service.AddSingleton<IEmailService, EmailService>();
        }
    }
}
