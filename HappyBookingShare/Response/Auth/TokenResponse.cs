namespace HappyBookingShare.Response.Auth;

public class TokenResponse
{
    public TokenResponse(long userId, string jwtToken, string refreshToken)
    {
        UserId = userId;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }

    public long UserId { get; private set; }

    public string JwtToken { get; private set; }

    public string RefreshToken { get; private set; }
}
