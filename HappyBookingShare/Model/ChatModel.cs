using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class ChatModel
{
    public ChatModel(Chat entity, List<ChatMemberModel> chatMemberList)
    {
        ChatId = entity.ChatId;
        ChatName = entity.ChatName;
        IsGroupChat = entity.IsGroupChat;
        GroupAvatar = entity.GroupAvatar;
        CreatedDate = entity.CreatedDate;
        LastChatTime = entity.LastChatTime;
        ChatMemberList = chatMemberList;
    }

    public ChatModel()
    {
    }

    public long ChatId { get; private set; }

    public string ChatName { get; private set; } = string.Empty;

    public bool IsGroupChat { get; private set; }

    public string GroupAvatar { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }

    public DateTime LastChatTime { get; private set; }

    public List<ChatMemberModel> ChatMemberList { get; private set; } = new();
}
