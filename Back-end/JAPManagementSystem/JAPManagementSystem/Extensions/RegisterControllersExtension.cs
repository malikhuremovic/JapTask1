using System.Text.Json.Serialization;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterControllersExtension
    {
        public static void RegisterControllers(this IServiceCollection service)
        {
            service.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }
    }
}
