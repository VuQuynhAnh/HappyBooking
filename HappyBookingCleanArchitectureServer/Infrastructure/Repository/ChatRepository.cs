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

    /// <summary>
    /// SaveGroupMember
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="chatMemberModelList"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> SaveGroupMember(long chatId, List<ChatMemberModel> chatMemberModelList, long userId)
    {
        List<ChatMember> memberAddNewList = new();
        var memberExistList = await _context.ChatMemberRepository.Where(item => item.ChatId == chatId
                                                                                && item.IsDeleted == 0).ToListAsync();

        foreach (var model in chatMemberModelList)
        {
            var memberEntity = memberExistList.FirstOrDefault(item => item.MemberId == model.MemberId);
            if (memberEntity == null)
            {
                if (model.IsDeleted != 0)
                {
                    continue;
                }
                memberEntity = new ChatMember()
                {
                    Id = 0,
                    ChatId = chatId,
                    MemberId = model.MemberId,
                    ChatRole = model.ChatRole,
                    IsDeleted = 0,
                    CreatedDate = DateTime.UtcNow,
                    CreatedId = userId,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedId = userId,
                };
                memberAddNewList.Add(memberEntity);
                continue;
            }
            if (model.IsDeleted == 1)
            {
                memberEntity.IsDeleted = 1;
                memberEntity.UpdatedDate = DateTime.UtcNow;
                memberEntity.UpdatedId = userId;
            }
        }

        await _context.ChatMemberRepository.AddRangeAsync(memberAddNewList);
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// GetMessageList
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="keyword"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<MessageModel>> GetMessageList(long chatId, string keyword, int pageIndex, int pageSize)
    {
        var messageList = await _context.MessageRepository
                                        .Where(item => item.ChatId == chatId
                                                       && item.Content.Contains(keyword)
                                                       && item.IsDeleted == 0)
                                        .OrderByDescending(item => item.CreatedDate)
                                        .Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

        var userIdList = messageList.Select(item => item.CreatedId)
                                    .Concat(messageList.Select(item => item.UpdatedId))
                                    .Distinct()
                                    .ToList();

        var userList = await _context.UserRepository.Where(item => userIdList.Contains(item.UserId)
                                                                   && item.IsDeleted == 0)
                                                    .ToListAsync();

        var result = messageList.Select(item => new MessageModel(item, userList))
                                .ToList();
        return result;
    }

    /// <summary>
    /// GetMessagesbyMessageId
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    public async Task<MessageModel> GetMessagesByMessageId(long messageId)
    {
        var message = await _context.MessageRepository
                                    .FirstOrDefaultAsync(item => item.MessageId == messageId
                                                                 && item.IsDeleted == 0);

        if (message == null)
        {
            return new();
        }

        var messageHistoryList = await _context.MessageHistoryRepository
                                               .Where(item => item.MessageId == message.MessageId)
                                               .ToListAsync();

        var userList = await _context.UserRepository.Where(item => (item.UserId == message.CreatedId
                                                                    || item.UserId == message.UpdatedId)
                                                                   && item.IsDeleted == 0)
                                                    .ToListAsync();

        var result = new MessageModel(message, messageHistoryList, userList);
        return result;
    }

    /// <summary>
    /// LeaveChatGroup
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="memberId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> LeaveChatGroup(long chatId, long memberId, long userId)
    {
        var chatMember = await _context.ChatMemberRepository
                                            .FirstOrDefaultAsync(item => item.ChatId == chatId
                                                                         && item.MemberId == memberId
                                                                         && item.IsDeleted == 0);
        if (chatMember == null)
        {
            return false;
        }
        chatMember.IsDeleted = 1;
        chatMember.UpdatedDate = DateTime.UtcNow;
        chatMember.UpdatedId = userId;
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// SaveChatGroup
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="name"></param>
    /// <param name="avatarUrl"></param>
    /// <param name="isGroup"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<ChatModel> SaveChatGroup(long chatId, string name, string avatarUrl, bool isGroup, long userId)
    {
        var chat = await _context.ChatRepository.FirstOrDefaultAsync(item => item.ChatId == chatId && item.IsDeleted == 0);
        if (chat == null)
        {
            chat = new();
            chat.ChatId = 0;
            chat.CreatedDate = DateTime.UtcNow;
            chat.CreatedId = userId;
            chat.LastChatTime = DateTime.UtcNow;
        }
        chat.ChatName = name;
        chat.GroupAvatar = avatarUrl;
        chat.IsGroupChat = isGroup;
        chat.IsDeleted = 0;
        chat.UpdatedDate = DateTime.UtcNow;
        chat.UpdatedId = userId;
        if (chat.ChatId == 0)
        {
            await _context.ChatRepository.AddAsync(chat);
        }
        await _context.SaveChangesAsync();
        return new ChatModel(chat);
    }

    /// <summary>
    /// GetChatGroup
    /// </summary>
    /// <param name="chatId"></param>
    /// <returns></returns>
    public async Task<ChatModel> GetChatGroup(long chatId)
    {

        var chat = await _context.ChatRepository.FirstOrDefaultAsync(item => item.ChatId == chatId && item.IsDeleted == 0);
        if (chat == null)
        {
            return new();
        }

        var chatMemberList = await (from chatMember in _context.ChatMemberRepository.Where(item => item.ChatId == chatId && item.IsDeleted == 0)
                                    join user in _context.UserRepository.Where(item => item.IsDeleted == 0)
                                    on chatMember.MemberId equals user.UserId into userJoin
                                    from userJoinMember in userJoin.DefaultIfEmpty()
                                    select new ChatMemberModel(chatMember, userJoinMember)
                                    ).ToListAsync();
        return new ChatModel(chat, chatMemberList);
    }

    /// <summary>
    /// CheckExistMemberInGroupChat
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<bool> CheckExistMemberInGroupChat(long chatId, long memberId)
    {
        var isMemberExistInGroup = await _context.ChatMemberRepository.AnyAsync(item => item.ChatId == chatId
                                                                                        && item.MemberId == memberId
                                                                                        && item.IsDeleted == 0);
        return isMemberExistInGroup;
    }

    /// <summary>
    /// GetListChatGroupByMember
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="keyword"></param>
    /// <param name="isGroupChat"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<ChatModel>> GetListChatGroupByMember(long memberId, string keyword, bool isGroupChat, int pageIndex, int pageSize)
    {
        var chatIdList = await _context.ChatMemberRepository
                                        .Where(item => item.MemberId == memberId && item.IsDeleted == 0)
                                        .Select(item => item.ChatId)
                                        .Distinct()
                                        .ToListAsync();

        if (!chatIdList.Any())
        {
            return new();
        }

        var chatList = await _context.ChatRepository
                                     .Where(item => chatIdList.Contains(item.ChatId)
                                                    && item.ChatName.Contains(keyword)
                                                    && item.IsDeleted == 0
                                                    && item.IsGroupChat == isGroupChat)
                                     .OrderByDescending(item => item.LastChatTime)
                                     .Skip((pageIndex - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

        var filteredChatIdList = chatList.Select(chat => chat.ChatId).Distinct().ToList();

        var chatMembers = await _context.ChatMemberRepository
                                        .Where(item => filteredChatIdList.Contains(item.ChatId) && item.IsDeleted == 0)
                                        .ToListAsync();

        var userIdSet = new HashSet<long>(chatMembers.Select(member => member.MemberId));

        var userList = await _context.UserRepository
                                     .Where(user => userIdSet.Contains(user.UserId) && user.IsDeleted == 0)
                                     .ToListAsync();

        var userDictionary = userList.ToDictionary(user => user.UserId);

        var chatMemberList = chatMembers.Select(member => new ChatMemberModel(member, userDictionary.ContainsKey(member.MemberId) ? userDictionary[member.MemberId] : new()))
                                        .ToList();

        var result = chatList.Select(chat => new ChatModel(chat, chatMemberList.Where(member => member.ChatId == chat.ChatId).ToList()))
                             .ToList();

        return result;
    }

    /// <summary>
    /// SendMessage
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="content"></param>
    /// <param name="messageType"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<MessageModel> SendMessage(long chatId, string content, int messageType, long userId)
    {
        var chat = await _context.ChatRepository.FirstOrDefaultAsync(item => item.ChatId == chatId && item.IsDeleted == 0);
        if (chat == null)
        {
            return new();
        }
        // update chat
        chat.LastChatTime = DateTime.UtcNow;

        var message = new Message()
        {
            ChatId = chatId,
            Content = content,
            MessageType = messageType,
            CreatedDate = DateTime.UtcNow,
            CreatedId = userId,
            UpdatedDate = DateTime.UtcNow,
            UpdatedId = userId,
        };
        await _context.MessageRepository.AddAsync(message);
        await _context.SaveChangesAsync();

        var userList = await _context.UserRepository.Where(item => (item.UserId == message.CreatedId
                                                                    || item.UserId == message.UpdatedId)
                                                                   && item.IsDeleted == 0)
                                                    .ToListAsync();
        return new MessageModel(message, userList);
    }

    /// <summary>
    /// UpdateMessage
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="content"></param>
    /// <param name="messageType"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<MessageModel> UpdateMessage(long messageId, string content, int messageType, long userId)
    {
        var message = await _context.MessageRepository.FirstOrDefaultAsync(item => item.MessageId == messageId && item.IsDeleted == 0);
        if (message == null)
        {
            return new();
        }

        // add message history
        MessageHistory messageHistory = new()
        {
            HistoryId = 0,
            MessageId = messageId,
            ChatId = message.ChatId,
            MessageType = message.MessageType,
            Content = message.Content,
            CreatedDate = DateTime.UtcNow,
            CreatedId = userId,
        };
        await _context.MessageHistoryRepository.AddAsync(messageHistory);

        // update message
        message.Content = content;
        message.MessageType = messageType;
        message.UpdatedDate = DateTime.UtcNow;
        message.UpdatedId = userId;
        await _context.SaveChangesAsync();

        var userList = await _context.UserRepository.Where(item => (item.UserId == message.CreatedId
                                                                    || item.UserId == message.UpdatedId)
                                                                   && item.IsDeleted == 0)
                                                    .ToListAsync();
        var messageHistoryList = await _context.MessageHistoryRepository.Where(item => item.MessageId == messageId).ToListAsync();
        return new MessageModel(message, messageHistoryList, userList);
    }

    public async Task ReleaseResource()
    {
        await _context.DisposeAsync();
    }

    /// <summary>
    /// CheckExistChat
    /// </summary>
    /// <param name="chatId"></param>
    /// <returns></returns>
    public async Task<bool> CheckExistChat(long chatId)
    {
        return await _context.ChatRepository.AnyAsync(item => item.ChatId == chatId && item.IsDeleted == 0);
    }

    /// <summary>
    /// Get1v1ExistGroupChatId
    /// </summary>
    /// <param name="memberIdList"></param>
    /// <returns></returns>
    public async Task<long> Get1v1ExistGroupChatId(long firstMemberId, long secondMemberId)
    {
        long chatId = 0;
        var firstChatIdList = await _context.ChatMemberRepository.Where(item => item.MemberId == firstMemberId && item.IsDeleted == 0)
                                                                 .Select(item => item.ChatId)
                                                                 .Distinct()
                                                                 .ToListAsync();
        if (!firstChatIdList.Any())
        {
            return chatId;
        }

        var commonChatIdList = await _context.ChatMemberRepository.Where(item => item.MemberId == secondMemberId
                                                                                 && firstChatIdList.Contains(item.ChatId)
                                                                                 && item.IsDeleted == 0)
                                                                  .Select(item => item.ChatId)
                                                                  .Distinct()
                                                                  .ToListAsync();
        if (!commonChatIdList.Any())
        {
            return chatId;
        }

        var chatIdResult = await _context.ChatMemberRepository.Where(item => commonChatIdList.Contains(item.ChatId)
                                                                             && item.IsDeleted == 0)
                                                              .GroupBy(item => item.ChatId)
                                                              .Select(item => new { item.Key, Count = item.Count() })
                                                              .Where(item => item.Count == 2)
                                                              .ToListAsync();
        if (!chatIdResult.Any())
        {
            return chatId;
        }

        chatId = chatIdResult.First().Key;
        return chatId;
    }

    /// <summary>
    /// DeleteChatGroup
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteChatGroup(long chatId, long userId)
    {
        var chatGroup = await _context.ChatRepository.FirstOrDefaultAsync(item => item.ChatId == chatId && item.IsDeleted == 0);
        if (chatGroup == null)
        {
            return false;
        }
        chatGroup.IsDeleted = 1;
        chatGroup.UpdatedDate = DateTime.UtcNow;
        chatGroup.UpdatedId = userId;
        return await _context.SaveChangesAsync() > 0;
    }
}
