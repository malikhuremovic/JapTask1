using Hangfire;
using Hangfire.MemoryStorage;
using JAPManagement.Services.Services.HangfireServices;

namespace JAPManagement.API.Extensions
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
