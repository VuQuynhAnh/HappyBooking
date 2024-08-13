namespace HappyBookingShare.Request.Chat;

public class UpdateMessageRequest
{
    public long MessageId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int MessageType { get; set; }
}
