using JAPManagementSystem.AutoMapperMaps;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterAutoMapperExtension
    {
    public static void RegisterAutoMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(CommentMap));
            service.AddAutoMapper(typeof(JapProgramMap));
            service.AddAutoMapper(typeof(SelectionMap));
            service.AddAutoMapper(typeof(StudentMap));

        }
    }
}
