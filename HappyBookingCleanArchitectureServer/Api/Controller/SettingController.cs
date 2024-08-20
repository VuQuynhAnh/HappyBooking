using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class SettingController : BaseController
{
    private readonly IGetSettingByUserIdUseCase _getSettingByUserIdUseCase;
    private readonly ISaveSettingUseCase _saveSettingUseCase;

    public SettingController(
        IGetSettingByUserIdUseCase getSettingByUserIdUseCase,
        ISaveSettingUseCase saveSettingUseCase,
        IHubContext<ChatHub> hubContext,
        IHttpContextAccessor httpContextAccessor,
        IHeartbeatUserUseCase heartbeatUserUseCase) : base(httpContextAccessor, heartbeatUserUseCase, hubContext)
    {
        _getSettingByUserIdUseCase = getSettingByUserIdUseCase;
        _saveSettingUseCase = saveSettingUseCase;
    }

    [HttpGet(APIName.GetSetting)]
    public async Task<ActionResult<GetSettingResponse>> GetSetting()
    {
        var response = await _getSettingByUserIdUseCase.GetSettingByUserId(UserId);
        return Ok(response);
    }

    [HttpPost(APIName.SaveSetting)]
    public async Task<ActionResult<SaveSettingResponse>> SaveSetting([FromBody] SaveSettingRequest request)
    {
        var response = await _saveSettingUseCase.SaveSetting(request, UserId);
        return Ok(response);
    }
}
