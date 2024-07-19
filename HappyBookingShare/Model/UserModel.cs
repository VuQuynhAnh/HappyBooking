using HappyBookingShare.Entities;

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
        AvatarImage = entity.AvatarImage;
    }

    public UserModel()
    {
    }

    public UserModel(long userId, string fullName, string email, string phoneNumber, string citizenIdentificationNumber, string address, string avatarImage, string password)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        CitizenIdentificationNumber = citizenIdentificationNumber;
        Address = address;
        AvatarImage = avatarImage;
        Password = password;
    }

    public long UserId { get; private set; }

    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public string CitizenIdentificationNumber { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string AvatarImage { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;
}
