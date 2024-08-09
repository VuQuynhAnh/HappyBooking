using HappyBookingShare.Model;

namespace HappyBookingShare.Response.Dtos;

public class MessageDto
{
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

    public long MessageId { get; private set; }

    public long ChatId { get; private set; }

    public int MessageType { get; private set; }

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }

    public long CreatedId { get; private set; }

    public DateTime UpdatedDate { get; private set; }

    public long UpdatedId { get; private set; }

    public UserDto CreateUser { get; set; }

    public UserDto UpdateUser { get; set; }

    public List<MessageHistoryDto> MessageHistoryList { get; private set; } = new();
}
