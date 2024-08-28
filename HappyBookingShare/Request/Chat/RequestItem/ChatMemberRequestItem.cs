namespace HappyBookingShare.Request.Chat.RequestItem;

public class ChatMemberRequestItem
{
    public ChatMemberRequestItem(long memberId, int chatRole)
    {
        MemberId = memberId;
        ChatRole = chatRole;
    }

    public ChatMemberRequestItem(long memberId, int chatRole, int isDeleted)
    {
        MemberId = memberId;
        ChatRole = chatRole;
        IsDeleted = isDeleted;
    }

    public long MemberId { get; set; }

    public int ChatRole { get; set; }

    public int IsDeleted { get; set; }
}
