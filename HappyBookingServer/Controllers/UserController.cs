using DemoBuildCoreProject.Business.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HappyBookingShare.Response.User;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using DemoBuildCoreProject.Controllers;

namespace DemoBuildCoreProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userService = userService;
    }

    [HttpGet(APIName.GetAllData)]
    public async Task<ActionResult<GetListUserResponse>> GetData([FromQuery] GetListUserRequest request)
    {
        var response = await _userService.GetAllUserData(UserId, request);
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

    [HttpPost(APIName.RegisterUser)]
    [AllowAnonymous]
    public async Task<ActionResult<SaveUserResponse>> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var response = await _userService.RegisterUser(UserId, request);
        return Ok(response);
    }

    [HttpPut(APIName.UpdateUser)]
    public async Task<ActionResult<SaveUserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var response = await _userService.UpdateUser(UserId, request);
        return Ok(response);
    }
}
