using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IService;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;
using HappyBookingShare.Response.Dtos;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Text.Json;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Auth;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> Login(LoginRequest request, IHubContext<ChatHub> hubContext)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        long userId = 0;
        StatusEnum status = StatusEnum.Successed;
        try
        {
            var user = await _userRepository.Login(request.UserName, request.Password);
            if (user.UserId > 0)
            {
                var tokenResponse = await _tokenService.GenerateToken(user);
                token = tokenResponse.JwtToken;
                refeshToken = tokenResponse.RefreshToken;
                userId = user.UserId;
                var userDto = new UserDto(user);
                string jsonString = JsonSerializer.Serialize(new List<UserDto>() { userDto });
                await hubContext.Clients.All.SendAsync(RealtimeConstant.UserStatus, jsonString);
            }
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }

        return new LoginResponse(userId, token, refeshToken, status);
    }
}
