using DemoBuildCoreProject.Model;
using DemoBuildCoreProject.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBuildCoreProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> GetData()
    {
        var data = await _userService.GetAllUserList();
        return Ok(data);
    }
}
