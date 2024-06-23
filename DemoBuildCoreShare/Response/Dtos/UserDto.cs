using HappyBookingShare.Model;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Dtos;

public class UserDto
{
    public UserDto(UserModel model)
    {
        UserId = model.UserId;
        UserName = model.UserName;
        Description = model.Description;
    }

    public UserDto()
    {
    }

    [JsonConstructor]
    public UserDto(long userId, string userName, string description)
    {
        UserId = userId;
        UserName = userName;
        Description = description;
    }

    [JsonPropertyName("userId")]
    public long UserId { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
