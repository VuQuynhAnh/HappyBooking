using HappyBookingShare.Entities;

namespace HappyBookingShare.Model;

public class ChatModel
{
    public ChatModel(long chatId, string chatName, bool isGroupChat, string groupAvatar, DateTime createdDate, long createdId, UserModel hostMember, List<UserModel> memberList)
    {
        ChatId = chatId;
        ChatName = chatName;
        IsGroupChat = isGroupChat;
        GroupAvatar = groupAvatar;
        CreatedDate = createdDate;
        CreatedId = createdId;
        HostMember = hostMember;
        MemberList = memberList;
    }

    public ChatModel(Chat entity, List<User> memberList)
    {
        ChatId = entity.ChatId;
        ChatName = entity.ChatName;
        IsGroupChat = entity.IsGroupChat;
        GroupAvatar = entity.GroupAvatar;
        CreatedDate = entity.CreatedDate;
        CreatedId = entity.CreatedId;
        var memberModelList = memberList.Select(item => new UserModel(item)).ToList();
        HostMember = memberModelList.FirstOrDefault(item => item.UserId == CreatedId) ?? new();
        MemberList = memberModelList.Where(item => item.UserId != CreatedId).ToList();
    }

    public ChatModel()
    {
    }

    public long ChatId { get; private set; }

    public string ChatName { get; private set; } = string.Empty;

    public bool IsGroupChat { get; private set; }

    public string GroupAvatar { get; private set; } = string.Empty;

    public DateTime CreatedDate { get; private set; }

    public long CreatedId { get; private set; }

    public UserModel HostMember { get; private set; } = new();

    public List<UserModel> MemberList { get; private set; } = new();
}
