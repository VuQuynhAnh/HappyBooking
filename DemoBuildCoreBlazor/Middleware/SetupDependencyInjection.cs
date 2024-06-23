using DemoBuildCoreBlazor.ApiService;
using DemoBuildCoreBlazor.ApiService.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace DemoBuildCoreBlazor.Middleware;

public static class SetupDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddScoped<IUserService, UserService>();
    }
}
