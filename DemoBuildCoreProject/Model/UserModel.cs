using DemoBuildCoreProject.Entities;

namespace DemoBuildCoreProject.Model;

public class UserModel
{
    public UserModel(int id, string name, string password, string description)
    {
        Id = id;
        Name = name;
        Password = password;
        Description = description;
    }

    public UserModel(User entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Password = entity.Password;
        Description = entity.Description;
    }

    public UserModel()
    {
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
}
