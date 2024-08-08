using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class MessageModel
{
    public MessageModel()
    {
    }

    public MessageModel(Message entity, List<MessageHistory> messageHistoryList)
    {
        MessageId = entity.MessageId;
        ChatId = entity.ChatId;
        MessageType = entity.MessageType;
        Content = entity.Content;
        CreatedDate = entity.CreatedDate;
        CreatedId = entity.CreatedId;
        UpdatedDate = entity.UpdatedDate;
        UpdatedId = entity.UpdatedId;
        MessageHistoryList = messageHistoryList.Select(item => new MessageHistoryModel(item)).ToList();
    }

    public MessageModel(Message entity)
    {
        MessageId = entity.MessageId;
        ChatId = entity.ChatId;
        MessageType = entity.MessageType;
        Content = entity.Content;
        CreatedDate = entity.CreatedDate;
        CreatedId = entity.CreatedId;
        UpdatedDate = entity.UpdatedDate;
        UpdatedId = entity.UpdatedId;
    }

    public long MessageId { get; private set; }

    public long ChatId { get; private set; }

    public int MessageType { get; private set; }

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }

    public long CreatedId { get; private set; }

    public DateTime UpdatedDate { get; private set; }

    public long UpdatedId { get; private set; }

    public List<MessageHistoryModel> MessageHistoryList { get; private set; } = new();
}
