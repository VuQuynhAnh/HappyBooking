﻿using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IService;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingCleanArchitectureServer.Core.Service;
using HappyBookingCleanArchitectureServer.Core.UseCase.Auth;
using HappyBookingCleanArchitectureServer.Core.UseCase.Chat;
using HappyBookingCleanArchitectureServer.Core.UseCase.Image;
using HappyBookingCleanArchitectureServer.Core.UseCase.Setting;
using HappyBookingCleanArchitectureServer.Core.UseCase.User;
using HappyBookingCleanArchitectureServer.Infrastructure.Repository;

namespace HappyBookingCleanArchitectureServer.Middleware;

public static class SetupDependencyInjection
{
    public static void AddUseCaseService(this IServiceCollection services)
    {
        // Auth
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        // Image
        services.AddHttpClient<IUploadImageUseCase, UploadImageUseCase>();
        services.AddScoped<IClearImageNotUsedUseCase, ClearImageNotUsedUseCase>();
        services.AddScoped<IDeleteImageUseCase, DeleteImageUseCase>();

        // Setting
        services.AddScoped<IGetSettingByUserIdUseCase, GetSettingByUserIdUseCase>();
        services.AddScoped<ISaveSettingUseCase, SaveSettingUseCase>();

        // User
        services.AddScoped<IGetAllUserDataUseCase, GetAllUserDataUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IGetUserByUserIdUseCase, GetUserByUserIdUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IHeartbeatUserUseCase, HeartbeatUserUseCase>();

        // Chat
        services.AddScoped<IAddMemberToGroupUseCase, AddMemberToGroupUseCase>();
        services.AddScoped<IGetMessageListUseCase, GetMessageListUseCase>();
        services.AddScoped<IGetMessagesByMessageIdUseCase, GetMessagesByMessageIdUseCase>();
        services.AddScoped<ILeaveChatGroupUseCase, LeaveChatGroupUseCase>();
        services.AddScoped<ISaveChatGroupUseCase, SaveChatGroupUseCase>();
        services.AddScoped<IGetChatGroupUseCase, GetChatGroupUseCase>();
        services.AddScoped<IGetListChatGroupByMemberUseCase, GetListChatGroupByMemberUseCase>();
        services.AddScoped<ISendMessageUseCase, SendMessageUseCase>();
        services.AddScoped<IUpdateMessageUseCase, UpdateMessageUseCase>();
    }

    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<ISettingRepository, SettingRepository>();
        services.AddTransient<IChatRepository, ChatRepository>();
    }
}
