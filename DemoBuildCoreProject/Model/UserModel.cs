using DemoBuildCoreProject.Entities;

namespace DemoBuildCoreProject.Model;

public class UserModel
{
    public UserModel(long userId, string userName, string password, string description)
    {
        UserId = userId;
        UserName = userName;
        Password = password;
        Description = description;
    }

    public UserModel(User entity)
    {
        UserId = entity.UserId;
        UserName = entity.UserName;
        Password = entity.Password;
        Description = entity.Description;
    }

    public UserModel()
    {
    }

    public long UserId { get; private set; }

    public string UserName { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
}
