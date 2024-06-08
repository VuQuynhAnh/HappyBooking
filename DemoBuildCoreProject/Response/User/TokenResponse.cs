namespace DemoBuildCoreProject.Response.User;

public class TokenResponse
{
    public TokenResponse(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }

    public string JwtToken { get; private set; }

    public string RefreshToken { get; private set; }
}
