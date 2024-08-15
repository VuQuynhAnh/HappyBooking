﻿using HappyBookingClient.Service;
using HappyBookingClient.Service.IService;

namespace HappyBookingClient.Middleware;

public static class SetupDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IUploadImageService, UploadImageService>();
        services.AddScoped<IChatService, ChatService>();
    }
}
