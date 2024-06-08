namespace DemoBuildCoreProject.Request.User;

public class RefreshTokenRequest
{
    public string JwtToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
