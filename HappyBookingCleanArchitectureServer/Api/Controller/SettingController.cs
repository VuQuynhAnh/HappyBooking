using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;
using Microsoft.AspNetCore.Mvc;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class SettingController : BaseController
{
    private readonly IGetSettingByUserIdUseCase _getSettingByUserIdUseCase;
    private readonly ISaveSettingUseCase _saveSettingUseCase;

    public SettingController(IGetSettingByUserIdUseCase getSettingByUserIdUseCase, ISaveSettingUseCase saveSettingUseCase, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
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
