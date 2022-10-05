using System.Runtime.CompilerServices;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterCorsExtension
    {
        public static string origin = "Front-End-React-App";
        public static void RegisterCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(name: origin,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }
    }
}
