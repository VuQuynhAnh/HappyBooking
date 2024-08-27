using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface ISaveChatGroupUseCase
{
    Task<SaveChatGroupResponse> SaveChatGroup(long userId, SaveChatGroupRequest request, IHubContext<ChatHub> hubContext);
}
