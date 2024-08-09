using HappyBookingShare.Request.Chat.RequestItem;

namespace HappyBookingShare.Request.Chat;

public class AddMemberToGroupRequest
{
    public long ChatId { get; set; }

    public List<ChatMemberRequestItem> ChatMemberList { get; set; } = new();
}
