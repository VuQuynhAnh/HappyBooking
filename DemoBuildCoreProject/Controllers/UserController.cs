using DemoBuildCoreProject.Business.IService;
using Microsoft.AspNetCore.Mvc;
using DemoBuildCoreProject.Constant;
using DemoBuildCoreProject.Request.User;
using DemoBuildCoreProject.Response.User;
using Microsoft.AspNetCore.Authorization;

namespace DemoBuildCoreProject.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(APIName.GetAllData)]
    public async Task<ActionResult<GetListUserResponse>> GetData([FromQuery] GetListUserRequest request)
    {
        var response = await _userService.GetAllUserData(request);
        return Ok(response);
    }

    [HttpPost(APIName.Login)]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var response = await _userService.Login(request);
        return Ok(response);
    }

    [HttpPost(APIName.RefreshToken)]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _userService.RefreshToken(request);
        return Ok(response);
    }
}
