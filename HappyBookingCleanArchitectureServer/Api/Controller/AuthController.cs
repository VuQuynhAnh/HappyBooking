using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Auth;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Auth;
using HappyBookingShare.Response.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;
    private readonly IHubContext<ChatHub> _hubContext;

    public AuthController(ILoginUseCase loginUseCase, IRefreshTokenUseCase refreshTokenUseCase, IHubContext<ChatHub> hubContext)
    {
        _loginUseCase = loginUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
        _hubContext = hubContext;
    }

    [HttpPost(APIName.Login)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var response = await _loginUseCase.Login(request, _hubContext);
        return Ok(response);
    }

    [HttpPost(APIName.RefreshToken)]
    public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _refreshTokenUseCase.RefreshToken(request, _hubContext);
        return Ok(response);
    }
}
