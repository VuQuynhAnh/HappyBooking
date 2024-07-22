using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;
using HappyBookingShare.Common;
using HappyBookingShare.Model;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Setting;

public class SaveSettingUseCase : ISaveSettingUseCase
{
    private readonly ISettingRepository _settingRepository;
    private readonly IMemoryCache _cache;

    public SaveSettingUseCase(ISettingRepository settingRepository, IMemoryCache cache)
    {
        _settingRepository = settingRepository;
        _cache = cache;
    }

    /// <summary>
    /// SaveSetting
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<SaveSettingResponse> SaveSetting(SaveSettingRequest request, long userId)
    {
        try
        {
            StatusEnum status = StatusEnum.Successed;
            var model = new SettingModel(userId, request.LanguageCode);
            var result = await _settingRepository.SaveSetting(model);
            return new SaveSettingResponse(userId, result, status, _cache);
        }
        finally
        {
            await _settingRepository.ReleaseResource();
        }
    }
}
