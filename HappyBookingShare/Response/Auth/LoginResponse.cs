using HappyBookingShare.Common;
using System.Text.Json.Serialization;

namespace HappyBookingShare.Response.Auth;

public class LoginResponse
{
    public LoginResponse(StatusEnum status)
    {
        Status = status;
    }

    [JsonConstructor]
    public LoginResponse(long userId, string token, string refeshToken, StatusEnum status)
    {
        UserId = userId;
        Token = token;
        RefeshToken = refeshToken;
        Status = status;
    }

    public long UserId { get; private set; }

    public string Token { get; private set; } = string.Empty;

    public string RefeshToken { get; private set; } = string.Empty;

    public StatusEnum Status { get; private set; }

    public string Message
    {
        get => EnglishMessageConstant.GetMessage(Status);
    }
}
