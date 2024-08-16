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
        Role = model.Role;
        IsOnline = model.IsOnline;
    }

    public UserDto()
    {
    }

    [JsonConstructor]
    public UserDto(long userId, string fullName, string email, string phoneNumber, string citizenIdentificationNumber, string address, string avatarImage, int role, bool isOnline)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        CitizenIdentificationNumber = citizenIdentificationNumber;
        Address = address;
        AvatarImage = avatarImage;
        Role = role;
        IsOnline = isOnline;
    }

    [JsonPropertyName("userId")]
    public long UserId { get; private set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; private set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; private set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; private set; } = string.Empty;

    [JsonPropertyName("citizenIdentificationNumber")]
    public string CitizenIdentificationNumber { get; private set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; private set; } = string.Empty;

    [JsonPropertyName("avatarImage")]
    public string AvatarImage { get; private set; } = string.Empty;

    [JsonPropertyName("role")]
    public int Role { get; private set; }

    [JsonPropertyName("isOnline")]
    public bool IsOnline { get; private set; }
}
