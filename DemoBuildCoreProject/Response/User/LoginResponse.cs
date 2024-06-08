using DemoBuildCoreProject.Response.Status;

namespace DemoBuildCoreProject.Response.User;

public class LoginResponse
{
    public LoginResponse(string token, string refeshToken, string message, CommonStatus status)
    {
        Token = token;
        RefeshToken = refeshToken;
        Message = message;
        Status = status;
    }

    public string Token { get; private set; }

    public string RefeshToken { get; private set; }

    public string Message { get; private set; }

    public CommonStatus Status { get; private set; }
}
