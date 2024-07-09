using HappyBookingShare.Common;

namespace HappyBookingShare.Response.User;

public class LoginResponse
{
    public LoginResponse(string token, string refeshToken, StatusEnum status)
    {
        Token = token;
        RefeshToken = refeshToken;
        Status = status;
    }

    public string Token { get; private set; }

    public string RefeshToken { get; private set; }

    public StatusEnum Status { get; private set; }

    public string Message
    {
        get => EnglishMessageConstant.GetMessage(Status);
    }
}
