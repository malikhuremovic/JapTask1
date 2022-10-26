using System.Text.Json.Serialization;

namespace JAPManagement.API.Extensions
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
