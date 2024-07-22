using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;

public interface IRefreshTokenUseCase
{
    Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
}
