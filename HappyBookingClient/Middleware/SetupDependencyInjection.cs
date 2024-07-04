using HappyBookingClient.Service;
using HappyBookingClient.Service.IService;

namespace HappyBookingClient.Middleware;

public static class SetupDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ILanguageService, LanguageService>();
        services.AddScoped<IUserService, UserService>();
    }
}
