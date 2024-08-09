using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IGetChatGroupUseCase
{
    Task<GetChatGroupResponse> GetChatGroup(long userId, GetChatGroupRequest request);
}
