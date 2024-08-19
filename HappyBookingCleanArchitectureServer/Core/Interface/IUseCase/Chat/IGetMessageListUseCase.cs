using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IGetMessageListUseCase
{
    Task<GetMessageListResponse> GetMessageList(long userId, GetMessageListRequest request, IHubContext<ChatHub> hubContext);
}
