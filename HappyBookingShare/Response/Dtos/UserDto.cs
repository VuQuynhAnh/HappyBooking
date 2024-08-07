using HappyBookingShare.Model;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Dtos;

public class UserDto
{
    public UserDto(UserModel model)
    {
        UserId = model.UserId;
        FullName = model.FullName;
        Email = model.Email;
        PhoneNumber = model.PhoneNumber;
        CitizenIdentificationNumber = model.CitizenIdentificationNumber;
        Address = model.Address;
        AvatarImage = model.AvatarImage;
    }

    public UserDto()
    {
    }

    [JsonPropertyName("userId")]
    public long UserId { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("citizenIdentificationNumber")]
    public string CitizenIdentificationNumber { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("avatarImage")]
    public string AvatarImage { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public int Role { get; set; }
}
