using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IAddMemberToGroupUseCase
{
    Task<AddMemberToGroupResponse> AddMemberToGroup(long userId, AddMemberToGroupRequest request);
}
