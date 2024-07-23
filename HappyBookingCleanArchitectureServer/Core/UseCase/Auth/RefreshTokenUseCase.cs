using HappyBookingCleanArchitectureServer.Core.Interface.IService;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Auth;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly ITokenService _tokenService;

    public RefreshTokenUseCase(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    /// refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        long userId = 0;
        StatusEnum status = StatusEnum.Successed;
        var result = await _tokenService.RefreshTokenAsync(request.JwtToken, request.RefreshToken);
        if (result != null)
        {
            userId = result.UserId;
            token = result.JwtToken;
            refeshToken = result.RefreshToken;
        }

        return new LoginResponse(userId, token, refeshToken, status);
    }
}
