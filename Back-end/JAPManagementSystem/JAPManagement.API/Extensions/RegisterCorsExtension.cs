namespace JAPManagement.API.Extensions
{
    public static class RegisterCorsExtension
    {
        public static string origin = "Front-End-React-App";
        public static void RegisterCors(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(name: origin,
                                  policy =>
                                  {
                                      policy.WithOrigins(config.GetSection("ApplicationData:ClientUrl").Value)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }
    }
}
