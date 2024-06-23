namespace DemoBuildCoreBlazor.ApiService.Interface;

public interface IUserService
{
    Task<string?> Login(string username, string password);

    Task<string?> RefreshToken(string jwtToken, string refreshToken);
}
