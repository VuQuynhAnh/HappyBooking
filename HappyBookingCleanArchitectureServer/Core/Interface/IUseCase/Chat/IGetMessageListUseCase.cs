using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IGetMessageListUseCase
{
    Task<GetMessageListResponse> GetMessageList(long userId, GetMessageListRequest request);
}
