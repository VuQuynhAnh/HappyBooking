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

    public ChatMemberModel(long memberId, int chatRole, int isDeleted)
    {
        MemberId = memberId;
        ChatRole = chatRole;
        IsDeleted = isDeleted;
    }

    public long ChatId { get; private set; } = new();

    public long MemberId { get; private set; } = new();

    public int ChatRole { get; private set; } = ChatRoleConstant.Manager;

    public int IsDeleted { get; private set; } = 0;

    public UserModel UserInformation { get; private set; } = new();
}
