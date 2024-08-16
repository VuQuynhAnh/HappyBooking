using HappyBookingShare.Model;
using System.Text.Json.Serialization;

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
        LastChatTime = model.LastChatTime;
        ChatMemberList = model.ChatMemberList.Select(item => new ChatMemberDto(item)).ToList();
    }

    [JsonConstructor]
    public ChatDto(long chatId, string chatName, bool isGroupChat, string groupAvatar, DateTime createdDate, List<ChatMemberDto> chatMemberList, DateTime lastChatTime)
    {
        ChatId = chatId;
        ChatName = chatName;
        IsGroupChat = isGroupChat;
        GroupAvatar = groupAvatar;
        CreatedDate = createdDate;
        ChatMemberList = chatMemberList;
        LastChatTime = lastChatTime;
    }

    public ChatDto()
    {
    }

    [JsonPropertyName("chatId")]
    public long ChatId { get; private set; }

    [JsonPropertyName("chatName")]
    public string ChatName { get; private set; } = string.Empty;

    [JsonPropertyName("isGroupChat")]
    public bool IsGroupChat { get; private set; }

    [JsonPropertyName("groupAvatar")]
    public string GroupAvatar { get; private set; } = string.Empty;

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; private set; }

    [JsonPropertyName("chatMemberList")]
    public List<ChatMemberDto> ChatMemberList { get; private set; } = new();

    [JsonPropertyName("lastChatTime")]
    public DateTime LastChatTime { get; private set; }
}
