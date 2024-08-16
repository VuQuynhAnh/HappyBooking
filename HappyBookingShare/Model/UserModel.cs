using HappyBookingShare.Entities;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Model;

public class UserModel
{
    public UserModel(User entity)
    {
        UserId = entity.UserId;
        FullName = entity.FullName;
        Email = entity.Email;
        PhoneNumber = entity.PhoneNumber;
        CitizenIdentificationNumber = entity.CitizenIdentificationNumber;
        Address = entity.Address;
        Role = entity.Role;
        AvatarImage = entity.AvatarImage;
        IsOnline = entity.IsOnline;
    }

    public UserModel()
    {
    }

    [JsonConstructor]
    public UserModel(long userId, string fullName, string email, string phoneNumber, string citizenIdentificationNumber, string address, string avatarImage, int role, string password, bool isOnline)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        CitizenIdentificationNumber = citizenIdentificationNumber;
        Address = address;
        AvatarImage = avatarImage;
        Role = role;
        Password = password;
        IsOnline = isOnline;
    }

    public long UserId { get; private set; }

    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public string CitizenIdentificationNumber { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string AvatarImage { get; private set; } = string.Empty;

    public int Role { get; private set; }

    public string Password { get; private set; } = string.Empty;

    public bool IsOnline { get; private set; }
}
