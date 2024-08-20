using HappyBookingShare.Model;
using System.Text.Json.Serialization;

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

    [JsonConstructor]
    public ChatMemberDto(long chatId, long memberId, int chatRole, UserDto userInformation)
    {
        ChatId = chatId;
        MemberId = memberId;
        ChatRole = chatRole;
        UserInformation = userInformation;
    }

    public ChatMemberDto()
    {
    }

    public ChatMemberDto ChangeUserInformation(UserDto userDto)
    {
        UserInformation = userDto;
        return this;
    }

    [JsonPropertyName("chatId")]
    public long ChatId { get; private set; }

    [JsonPropertyName("memberId")]
    public long MemberId { get; private set; }

    [JsonPropertyName("chatRole")]
    public int ChatRole { get; private set; }

    [JsonPropertyName("userInformation")]
    public UserDto UserInformation { get; private set; } = new();
}
