using HappyBookingShare.Realtime;
using HappyBookingShare.Request.User;
using HappyBookingShare.Response.User;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

public interface IHeartbeatUserUseCase
{
    Task<HeartbeatUserResponse> HeartbeatUser(long userId, IHubContext<ChatHub> hubContext);
}
