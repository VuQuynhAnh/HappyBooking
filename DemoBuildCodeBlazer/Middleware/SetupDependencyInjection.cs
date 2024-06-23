using Microsoft.AspNetCore.Components.Authorization;

namespace DemoBuildCodeBlazer.Middleware;

public static class SetupDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    }
}
