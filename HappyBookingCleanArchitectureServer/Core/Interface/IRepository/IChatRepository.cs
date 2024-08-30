using HappyBookingShare.Model;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IRepository;

public interface IChatRepository : IRepositoryBase
{
    Task<ChatModel> SaveChatGroup(long chatId, string name, string avatarUrl, bool isGroup, long userId);

    Task<bool> DeleteChatGroup(long chatId, long userId);

    Task<List<ChatModel>> GetListChatGroupByMember(long memberId, string keyword, bool isGroupChat, int pageIndex, int pageSize);

    Task<ChatModel> GetChatGroup(long chatId);

    Task<bool> SaveGroupMember(long chatId, List<ChatMemberModel> chatMemberModelList, long userId);

    Task<bool> LeaveChatGroup(long chatId, long memberId, long userId);

    Task<List<MessageModel>> GetMessageList(long chatId, string keyword, int pageIndex, int pageSize);

    Task<MessageModel> GetMessagesByMessageId(long messageId);

    Task<MessageModel> SendMessage(long chatId, string content, int messageType, long userId);

    Task<MessageModel> UpdateMessage(long messageId, string content, int messageType, long userId);

    Task<bool> CheckExistChat(long chatId);

    Task<bool> CheckExistMemberInGroupChat(long chatId, long memberId);

    Task<long> Get1v1ExistGroupChatId(long firstMemberId, long secondMemberId);
}
