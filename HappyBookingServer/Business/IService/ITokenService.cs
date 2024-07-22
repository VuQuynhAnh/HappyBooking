using HappyBookingShare.Model;
using HappyBookingShare.Response.Auth;

namespace HappyBookingServer.Business.IService;

public interface ITokenService
{
    Task<TokenResponse> GenerateToken(UserModel user);

    Task<TokenResponse?> RefreshTokenAsync(string jwtToken, string refeshToken);
}
