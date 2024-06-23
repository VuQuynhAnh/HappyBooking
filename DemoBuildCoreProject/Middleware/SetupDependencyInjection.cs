using HappyBookingServer.Business.IService;
using HappyBookingServer.Business;
using HappyBookingServer.Interface;
using HappyBookingServer.Repository;

namespace HappyBookingServer.Middleware;

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
