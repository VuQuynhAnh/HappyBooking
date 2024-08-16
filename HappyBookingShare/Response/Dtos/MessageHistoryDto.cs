using HappyBookingShare.Model;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Dtos;

public class MessageHistoryDto
{
    public MessageHistoryDto(MessageHistoryModel model)
    {
        HistoryId = model.HistoryId;
        MessageId = model.MessageId;
        ChatId = model.ChatId;
        MessageType = model.MessageType;
        Content = model.Content;
        CreatedDate = model.CreatedDate;
    }

    [JsonConstructor]
    public MessageHistoryDto(long historyId, long messageId, long chatId, int messageType, string content, DateTime createdDate)
    {
        HistoryId = historyId;
        MessageId = messageId;
        ChatId = chatId;
        MessageType = messageType;
        Content = content;
        CreatedDate = createdDate;
    }

    public MessageHistoryDto()
    {
    }

    [JsonPropertyName("historyId")]
    public long HistoryId { get; private set; }

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
}
