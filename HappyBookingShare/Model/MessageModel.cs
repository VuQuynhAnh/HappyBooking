using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class MessageModel
{
    public MessageModel()
    {
    }

    public MessageModel(long messageId, long chatId, int typeMessage, string content, DateTime createdDate, long createdId, DateTime updatedDate, long updatedId)
    {
        MessageId = messageId;
        ChatId = chatId;
        TypeMessage = typeMessage;
        Content = content;
        CreatedDate = createdDate;
        CreatedId = createdId;
        UpdatedDate = updatedDate;
        UpdatedId = updatedId;
    }

    public MessageModel(Message entity, List<User> userList)
    {
        MessageId = entity.MessageId;
        ChatId = entity.ChatId;
        TypeMessage = entity.TypeMessage;
        Content = entity.Content;
        CreatedDate = entity.CreatedDate;
        CreatedId = entity.CreatedId;
        UpdatedDate = entity.UpdatedDate;
        UpdatedId = entity.UpdatedId;
        var userModelList = userList.Select(item => new UserModel(item)).ToList();
        CreatedUser = userModelList.FirstOrDefault(item => item.UserId == CreatedId) ?? new();
        UpdatedUser = userModelList.FirstOrDefault(item => item.UserId == UpdatedId) ?? new();
    }

    public long MessageId { get; private set; }

    public long ChatId { get; private set; }

    public int TypeMessage { get; private set; }

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }

    public long CreatedId { get; private set; }

    public DateTime UpdatedDate { get; private set; }

    public long UpdatedId { get; private set; }

    public UserModel CreatedUser { get; private set; } = new();

    public UserModel UpdatedUser { get; private set; } = new();
}
