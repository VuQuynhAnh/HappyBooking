namespace HappyBookingShare.Request.Chat;

public class GetListChatGroupByMemberRequest : CommonRequest
{
    public long MemberId { get; set; }

    public bool IsGroupChat { get; set; }
}
