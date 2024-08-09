using HappyBookingShare.Model;

namespace HappyBookingShare.Response.Dtos;

public class ChatMemberDto
{
    public ChatMemberDto(ChatMemberModel model)
    {
        ChatId = model.ChatId;
        MemberId = model.MemberId;
        ChatRole = model.ChatRole;
        UserInformation = new UserDto(model.UserInformation);
    }

    public long ChatId { get; private set; }

    public long MemberId { get; private set; }

    public int ChatRole { get; private set; }

    public UserDto UserInformation { get; private set; }
}
