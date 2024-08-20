using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface ISendMessageUseCase
{
    Task<SendMessageResponse> SendMessage(long userId, SendMessageRequest request, IHubContext<ChatHub> hubContext);
}
