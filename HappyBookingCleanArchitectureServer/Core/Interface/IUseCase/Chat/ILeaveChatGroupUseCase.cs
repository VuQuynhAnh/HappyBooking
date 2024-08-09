using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface ILeaveChatGroupUseCase
{
    Task<LeaveChatGroupResponse> LeaveChatGroup(long userId, LeaveChatGroupRequest request);
}
