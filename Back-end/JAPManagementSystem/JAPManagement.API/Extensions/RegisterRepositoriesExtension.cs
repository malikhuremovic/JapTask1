using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Repositories;

namespace JAPManagement.API.Extensions
{
    public static class RegisterRepositoriesExtension
    {
        public static void RegisterRepositories(this IServiceCollection service)
        {
            service.AddTransient<IItemRepository, ItemRepository>();
            service.AddTransient<IAuthRepository, AuthRepository>();
            service.AddTransient<ISelectionRepository, SelectionRepository>();
            service.AddTransient<IProgramRepository, ProgramRepository>();
            service.AddTransient<IProgramItemRepository, ProgramItemRepository>();
            service.AddTransient<IStudentRepository, StudentRepository>();
            service.AddTransient<IStudentItemRepository, StudentItemRepository>();
        }
    }
}
