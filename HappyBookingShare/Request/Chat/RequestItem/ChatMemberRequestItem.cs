namespace HappyBookingShare.Request.Chat.RequestItem;

public class ChatMemberRequestItem
{
    public ChatMemberRequestItem(long memberId, int chatRole)
    {
        MemberId = memberId;
        ChatRole = chatRole;
    }

    public long MemberId { get; set; }

    public int ChatRole { get; set; }
}
