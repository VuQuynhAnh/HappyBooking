using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface ILeaveChatGroupUseCase
{
    Task<LeaveChatGroupResponse> LeaveChatGroup(long userId, LeaveChatGroupRequest request, IHubContext<ChatHub> hubContext);
}
