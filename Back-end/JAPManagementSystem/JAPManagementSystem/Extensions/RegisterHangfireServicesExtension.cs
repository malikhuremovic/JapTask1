using Hangfire;
using Hangfire.MemoryStorage;
using JAPManagementSystem.Services;
using JAPManagementSystem.Services.HangfireServices;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterHangfireServicesExtension
    {
        public static void RegisterHangfire(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddHangfire(hangfire =>
            {
                hangfire.UseSqlServerStorage(config.GetConnectionString("DefaultConnection"));
                hangfire.UseSimpleAssemblyNameTypeSerializer();
                hangfire.UseRecommendedSerializerSettings();
                hangfire.UseMemoryStorage();
            });
            service.AddHangfireServer();
            service.AddHostedService<HangfireHostedService>();
        }
    }
}
