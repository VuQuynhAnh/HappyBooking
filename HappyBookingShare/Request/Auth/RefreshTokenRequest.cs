namespace HappyBookingShare.Request.Auth;

public class RefreshTokenRequest
{
    public RefreshTokenRequest(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }

    public RefreshTokenRequest()
    {
    }

    public string JwtToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
