using HappyBookingShare.Response.Dtos;

namespace HappyBookingShare.Request.Chat.RequestItem;

public class ChatMemberRequestInputTable
{
    public ChatMemberRequestInputTable(long memberId, int chatRole, UserDto userDto)
    {
        MemberId = memberId;
        ChatRole = chatRole;
        UserInformation = userDto;
    }

    public long MemberId { get; set; }

    public int ChatRole { get; set; }

    public int IsDeleted { get; set; }
    
    public UserDto UserInformation { get; set; } = new();
}
