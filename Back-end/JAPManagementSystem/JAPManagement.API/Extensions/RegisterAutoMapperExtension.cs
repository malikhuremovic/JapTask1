using JAPManagement.Core.AutoMapperMaps;

namespace JAPManagement.API.Extensions
{
    public static class RegisterAutoMapperExtension
    {
        public static void RegisterAutoMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(CommentMap));
            service.AddAutoMapper(typeof(JapProgramMap));
            service.AddAutoMapper(typeof(SelectionMap));
            service.AddAutoMapper(typeof(StudentMap));
            service.AddAutoMapper(typeof(UserMap));
            service.AddAutoMapper(typeof(ItemMap));
        }
    }
}
