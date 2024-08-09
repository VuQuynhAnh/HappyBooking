using HappyBookingShare.Model;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IRepository;

public interface IChatRepository : IRepositoryBase
{
    Task<bool> SaveChatGroup(long chatId, string name, string avatarUrl, bool isGroup, long userId);

    Task<List<ChatModel>> GetListChatGroupByMember(long memberId, string keyword, bool isGroupChat, int pageIndex, int pageSize);

    Task<ChatModel> GetChatGroup(long chatId, long memberId);

    Task<bool> AddMemberToGroup(long chatId, List<ChatMemberModel> chatMemberList, long userId);

    Task<bool> LeaveChatGroup(long chatId, long memberId, long userId);

    Task<List<MessageModel>> GetMessageList(long chatId, string keyword, int pageIndex, int pageSize);

    Task<MessageModel> GetMessagesByMessageId(long messageId);

    Task<MessageModel> SendMessage(long chatId, string content, int messageType, long userId);

    Task<MessageModel> UpdateMessage(long messageId, string content, int messageType, long userId);

    Task<bool> CheckExistChat(long chatId);
}
