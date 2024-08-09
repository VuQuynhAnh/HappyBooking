namespace HappyBookingShare.Request.Chat;

public class SaveChatGroupRequest
{
    public long ChatId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string AvatarUrl { get; set; } = string.Empty;

    public bool IsGroup { get; set; }
}
