using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class MessageHistoryModel
{
    public MessageHistoryModel(long historyId, long messageId, long chatId, int typeMessage, string content, DateTime createdDate)
    {
        HistoryId = historyId;
        MessageId = messageId;
        ChatId = chatId;
        MessageType = typeMessage;
        Content = content;
        CreatedDate = createdDate;
    }

    public MessageHistoryModel(MessageHistory enity)
    {
        HistoryId = enity.HistoryId;
        MessageId = enity.MessageId;
        ChatId = enity.ChatId;
        MessageType = enity.MessageType;
        Content = enity.Content;
        CreatedDate = enity.CreatedDate;
    }

    public MessageHistoryModel()
    {
    }

    public long HistoryId { get; private set; }

    public long MessageId { get; private set; }

    public long ChatId { get; private set; }

    public int MessageType { get; private set; }

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }
}
