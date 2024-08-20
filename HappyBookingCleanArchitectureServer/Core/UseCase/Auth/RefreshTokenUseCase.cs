using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IService;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingShare.Common;
using HappyBookingShare.Entities;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;
using HappyBookingShare.Response.Dtos;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Auth;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public RefreshTokenUseCase(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request, IHubContext<ChatHub> hubContext)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        long userId = 0;
        StatusEnum status = StatusEnum.Successed;
        var result = await _tokenService.RefreshTokenAsync(request.JwtToken, request.RefreshToken);
        if (result != null)
        {
            userId = result.UserId;
            token = result.JwtToken;
            refeshToken = result.RefreshToken;

            var user = await _userRepository.GetUserByUserId(userId);
            var userDto = new UserDto(user);
            string jsonString = JsonSerializer.Serialize(userDto);
            await hubContext.Clients.All.SendAsync(RealtimeConstant.UserOnline, jsonString);
        }

        return new LoginResponse(userId, token, refeshToken, status);
    }
}
