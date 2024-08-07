using HappyBookingShare.Model;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IRepository;

public interface IChatRepository
{
    Task<ChatModel> SaveChatGroup(string name, string avatarUrl, bool isGroup, List<long> memberList, long userId);

    Task<bool> AddMemberToGroup(long chatId, long memberId, long userId);

    Task LeaveChatGroup(long chatId, long userId);

    Task<MessageModel> SendMessage(long chatId, string content, long userId);

    Task<List<MessageModel>> GetMessageList(long chatId, string keyword, int pageIndex, int pageSize);

    Task<MessageModel> GetMessagesbyMessageId(long messageId);
}
