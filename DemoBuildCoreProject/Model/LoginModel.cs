namespace DemoBuildCoreProject.Model;

public class LoginModel
{
    public LoginModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public LoginModel()
    {
    }

    public string Username { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;
}
