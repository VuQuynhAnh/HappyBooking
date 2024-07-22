using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;

    public AuthController(ILoginUseCase loginUseCase, IRefreshTokenUseCase refreshTokenUseCase)
    {
        _loginUseCase = loginUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
    }

    [HttpPost(APIName.Login)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var response = await _loginUseCase.Login(request);
        return Ok(response);
    }

    [HttpPost(APIName.RefreshToken)]
    public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _refreshTokenUseCase.RefreshToken(request);
        return Ok(response);
    }
}
