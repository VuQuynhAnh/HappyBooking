using DemoBuildCoreProject.Business.IService;
using DemoBuildCoreProject.Business;
using DemoBuildCoreProject.Interface;
using DemoBuildCoreProject.Repository;

namespace DemoBuildCoreProject.Middleware;

public static class SetupDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddHttpClient<IUploadImageService, UploadImageService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISettingService, SettingService>();
    }

    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<ISettingRepository, SettingRepository>();
    }
}
