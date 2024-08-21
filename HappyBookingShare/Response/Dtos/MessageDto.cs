using HappyBookingShare.Model;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Dtos;

public class MessageDto
{
    public MessageDto()
    {
    }

    public MessageDto(MessageModel model)
    {
        MessageId = model.MessageId;
        ChatId = model.ChatId;
        MessageType = model.MessageType;
        Content = model.Content;
        CreatedDate = model.CreatedDate;
        CreatedId = model.CreatedId;
        UpdatedDate = model.UpdatedDate;
        UpdatedId = model.UpdatedId;
        CreateUser = new UserDto(model.CreateUser);
        UpdateUser = new UserDto(model.UpdateUser);
        MessageHistoryList = model.MessageHistoryList.Select(item => new MessageHistoryDto(item)).ToList();
    }

    [JsonConstructor]
    public MessageDto(long messageId, long chatId, int messageType, string content, DateTime createdDate, long createdId, DateTime updatedDate, long updatedId, UserDto createUser, UserDto updateUser, List<MessageHistoryDto> messageHistoryList)
    {
        MessageId = messageId;
        ChatId = chatId;
        MessageType = messageType;
        Content = content;
        CreatedDate = createdDate;
        CreatedId = createdId;
        UpdatedDate = updatedDate;
        UpdatedId = updatedId;
        CreateUser = createUser;
        UpdateUser = updateUser;
        MessageHistoryList = messageHistoryList;
    }

    [JsonPropertyName("messageId")]
    public long MessageId { get; private set; }

    [JsonPropertyName("chatId")]
    public long ChatId { get; private set; }

    [JsonPropertyName("messageType")]
    public int MessageType { get; private set; }

    [JsonPropertyName("content")]
    public string Content { get; private set; } = string.Empty;

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; private set; }

    [JsonPropertyName("createdId")]
    public long CreatedId { get; private set; }

    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; private set; }

    [JsonPropertyName("updatedId")]
    public long UpdatedId { get; private set; }

    [JsonPropertyName("createUser")]
    public UserDto CreateUser { get; set; } = new();

    [JsonPropertyName("updateUser")]
    public UserDto UpdateUser { get; set; } = new();

    [JsonPropertyName("messageHistoryList")]
    public List<MessageHistoryDto> MessageHistoryList { get; private set; } = new();
}
