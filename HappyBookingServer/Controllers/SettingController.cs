using HappyBookingServer.Business.IService;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;
using Microsoft.AspNetCore.Mvc;

namespace HappyBookingServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SettingController : BaseController
{
    private readonly ISettingService _settingService;
    public SettingController(ISettingService settingService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _settingService = settingService;
    }

    [HttpGet(APIName.GetSetting)]
    public async Task<ActionResult<GetSettingResponse>> GetSetting()
    {
        var response = await _settingService.GetSetting(UserId);
        return Ok(response);
    }

    [HttpPost(APIName.SaveSetting)]
    public async Task<ActionResult<SaveSettingResponse>> SaveSetting([FromBody] SaveSettingRequest request)
    {
        var response = await _settingService.SaveSetting(request, UserId);
        return Ok(response);
    }

}
