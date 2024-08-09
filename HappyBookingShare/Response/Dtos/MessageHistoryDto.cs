
using HappyBookingShare.Model;

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

    public long HistoryId { get; private set; }

    public long MessageId { get; private set; }

    public long ChatId { get; private set; }

    public int MessageType { get; private set; }

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }
}
