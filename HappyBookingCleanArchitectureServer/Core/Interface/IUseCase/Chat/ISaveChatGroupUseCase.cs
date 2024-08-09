using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface ISaveChatGroupUseCase
{
    Task<SaveChatGroupResponse> SaveChatGroup(long userId, SaveChatGroupRequest request);
}
