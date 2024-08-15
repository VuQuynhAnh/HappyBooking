using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingClient.Service.IService;

public interface IChatService : IBaseApiService
{
    Task<AddMemberToGroupResponse?> AddMemberToGroupAsync(AddMemberToGroupRequest request);

    Task<LeaveChatGroupResponse?> LeaveChatGroupAsync(LeaveChatGroupRequest request);

    Task<SaveChatGroupResponse?> SaveChatGroupAsync(SaveChatGroupRequest request);

    Task<SendMessageResponse?> SendMessageAsync(SendMessageRequest request);

    Task<UpdateMessageResponse?> UpdateMessageAsync(UpdateMessageRequest request);

    Task<GetMessageListResponse?> GetMessageListAsync(GetMessageListRequest request);

    Task<GetMessagesByMessageIdResponse?> GetMessagesByMessageIdAsync(GetMessagesByMessageIdRequest request);

    Task<GetChatGroupResponse?> GetChatGroupAsync(GetChatGroupRequest request);

    Task<GetListChatGroupByMemberResponse?> GetListChatGroupByMemberAsync(GetListChatGroupByMemberRequest request);
}
