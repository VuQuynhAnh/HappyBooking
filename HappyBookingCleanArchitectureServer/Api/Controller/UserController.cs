using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HappyBookingShare.Response.User;
using HappyBookingShare.Common;
using HappyBookingShare.Request.User;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IGetAllUserDataUseCase _getAllUserDataUseCase;
    private readonly IGetUserByUserIdUseCase _getUserByUserIdUseCase;
    private readonly IRegisterUserUseCase _registerUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IChangePasswordUseCase _changePasswordUseCase;

    public UserController(IGetAllUserDataUseCase getAllUserDataUseCase, IRegisterUserUseCase registerUserUseCase, IUpdateUserUseCase updateUserUseCase, IGetUserByUserIdUseCase getUserByUserIdUseCase, IHttpContextAccessor httpContextAccessor, IChangePasswordUseCase changePasswordUseCase) : base(httpContextAccessor)
    {
        _getAllUserDataUseCase = getAllUserDataUseCase;
        _registerUserUseCase = registerUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _getUserByUserIdUseCase = getUserByUserIdUseCase;
        _changePasswordUseCase = changePasswordUseCase;
    }

    [HttpGet(APIName.GetAllData)]
    public async Task<ActionResult<GetListUserResponse>> GetData([FromQuery] GetListUserRequest request)
    {
        var response = await _getAllUserDataUseCase.GetAllUserData(UserId, request);
        return Ok(response);
    }

    [HttpGet(APIName.GetUserByUserId)]
    public async Task<ActionResult<GetListUserResponse>> GetUserByUserId([FromQuery] GetUserByUserIdRequest request)
    {
        var response = await _getUserByUserIdUseCase.GetUserByUserId(UserId, request);
        return Ok(response);
    }

    [HttpPost(APIName.RegisterUser)]
    [AllowAnonymous]
    public async Task<ActionResult<SaveUserResponse>> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var response = await _registerUserUseCase.RegisterUser(UserId, request);
        return Ok(response);
    }

    [HttpPut(APIName.UpdateUser)]
    public async Task<ActionResult<SaveUserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var response = await _updateUserUseCase.UpdateUser(UserId, request);
        return Ok(response);
    }

    [HttpPut(APIName.ChangePassword)]
    public async Task<ActionResult<SaveUserResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var response = await _changePasswordUseCase.ChangePassword(UserId, request);
        return Ok(response);
    }
}
