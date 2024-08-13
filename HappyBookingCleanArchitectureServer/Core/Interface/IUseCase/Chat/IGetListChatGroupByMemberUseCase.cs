using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IGetListChatGroupByMemberUseCase
{
    Task<GetListChatGroupByMemberResponse> GetListChatGroupByMember(long userId, GetListChatGroupByMemberRequest request);
}
