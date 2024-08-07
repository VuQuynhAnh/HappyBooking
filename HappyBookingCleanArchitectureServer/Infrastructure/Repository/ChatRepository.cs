using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Database;
using HappyBookingShare.Entities;
using HappyBookingShare.Model;
using Microsoft.EntityFrameworkCore;

namespace HappyBookingCleanArchitectureServer.Infrastructure.Repository;

public class ChatRepository : IChatRepository
{
    private readonly DataContext _context;

    public ChatRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> AddMemberToGroup(long chatId, long memberId, long userId)
    {
        var chatParticipant = await _context.ChatParticipantRepository.FirstOrDefaultAsync(item => item.ChatId == chatId
                                                                                                   && item.MemberId == memberId
                                                                                                   && item.IsDeleted == 0);
        if (chatParticipant == null)
        {
            chatParticipant = new ChatParticipant();
            chatParticipant.Id = 0;
            chatParticipant.MemberId = memberId;
            chatParticipant.ChatId = chatId;
            chatParticipant.IsDeleted = 0;
            chatParticipant.CreatedDate = DateTime.UtcNow;
            chatParticipant.CreatedId = userId;
        }
        chatParticipant.UpdatedDate = DateTime.UtcNow;
        chatParticipant.UpdatedId = userId;
        if (chatParticipant.Id == 0)
        {
            _context.ChatParticipantRepository.Add(chatParticipant);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<List<MessageModel>> GetMessageList(long chatId, string keyword, int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<MessageModel> GetMessagesbyMessageId(long messageId)
    {
        throw new NotImplementedException();
    }

    public Task LeaveChatGroup(long chatId, long userId)
    {
        throw new NotImplementedException();
    }

    public Task<ChatModel> SaveChatGroup(string name, string avatarUrl, bool isGroup, List<long> memberList, long userId)
    {
        throw new NotImplementedException();
    }

    public Task<MessageModel> SendMessage(long chatId, string content, long userId)
    {
        throw new NotImplementedException();
    }
}
