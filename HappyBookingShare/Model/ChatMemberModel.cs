using HappyBookingShare.Common;
using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class ChatMemberModel
{
    public ChatMemberModel(ChatMember chatMember, User user)
    {
        ChatId = chatMember.ChatId;
        MemberId = chatMember.MemberId;
        ChatRole = chatMember.ChatRole;
        UserInformation = new UserModel(user);
    }

    public ChatMemberModel()
    {
    }

    public ChatMemberModel(long memberId, int chatRole)
    {
        MemberId = memberId;
        ChatRole = chatRole;
    }

    public long ChatId { get; private set; } = new();

    public long MemberId { get; private set; } = new();

    public int ChatRole { get; private set; } = ChatRoleConstant.Manager;

    public UserModel UserInformation { get; private set; } = new();
}
