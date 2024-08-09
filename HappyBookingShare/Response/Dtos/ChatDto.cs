using HappyBookingShare.Model;

namespace HappyBookingShare.Response.Dtos;

public class ChatDto
{
    public ChatDto(ChatModel model)
    {
        ChatId = model.ChatId;
        ChatName = model.ChatName;
        IsGroupChat = model.IsGroupChat;
        GroupAvatar = model.GroupAvatar;
        CreatedDate = model.CreatedDate;
        ChatMemberList = model.ChatMemberList.Select(item => new ChatMemberDto(item)).ToList();
    }

    public long ChatId { get; private set; }

    public string ChatName { get; private set; } = string.Empty;

    public bool IsGroupChat { get; private set; }

    public string GroupAvatar { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }

    public List<ChatMemberDto> ChatMemberList { get; private set; } = new();
}
