using DemoBuildCoreProject.Model;

namespace DemoBuildCoreProject.Response.Dtos;

public class UserDto
{
    public UserDto(UserModel model)
    {
        UserId = model.UserId;
        UserName = model.UserName;
        Description = model.Description;
    }

    public long UserId { get; private set; }

    public string UserName { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
}
