using HappyBookingShare.Model;
using HappyBookingShare.Response.Auth;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IService;

public interface ITokenService
{
    Task<TokenResponse> GenerateToken(UserModel user);

    Task<TokenResponse?> RefreshTokenAsync(string jwtToken, string refeshToken);
}
