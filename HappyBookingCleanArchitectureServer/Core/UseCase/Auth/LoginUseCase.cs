using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IService;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Auth;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        string token = string.Empty;
        string refeshToken = string.Empty;
        StatusEnum status = StatusEnum.Successed;
        try
        {
            var user = await _userRepository.GetUserByLoginInfor(request.UserName, request.Password);
            if (user.UserId > 0)
            {
                var tokenResponse = await _tokenService.GenerateToken(user);
                token = tokenResponse.JwtToken;
                refeshToken = tokenResponse.RefreshToken;
            }
        }
        finally
        {
            await _userRepository.ReleaseResource();
        }

        return new LoginResponse(token, refeshToken, status);
    }
}
