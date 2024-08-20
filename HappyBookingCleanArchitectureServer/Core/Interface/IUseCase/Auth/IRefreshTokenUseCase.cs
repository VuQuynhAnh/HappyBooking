using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;

public interface IRefreshTokenUseCase
{
    Task<LoginResponse> RefreshToken(RefreshTokenRequest request, IHubContext<ChatHub> hubContext);
}
