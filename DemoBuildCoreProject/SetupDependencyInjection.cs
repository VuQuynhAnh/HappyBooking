using DemoBuildCoreProject.Business.IService;
using DemoBuildCoreProject.Business;
using DemoBuildCoreProject.Interface;
using DemoBuildCoreProject.Repository;

namespace DemoBuildCoreProject;

public static class SetupDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
