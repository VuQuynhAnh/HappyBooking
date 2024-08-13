namespace HappyBookingShare.Request.Chat;

public class SendMessageRequest
{
    public long ChatId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int MessageType { get; set; }
}
