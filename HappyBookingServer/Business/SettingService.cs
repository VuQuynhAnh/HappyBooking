using DemoBuildCoreProject.Business.IService;
using DemoBuildCoreProject.Interface;
using HappyBookingShare.Common;
using HappyBookingShare.Model;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.Setting;
using Microsoft.Extensions.Caching.Memory;

namespace DemoBuildCoreProject.Business;

public class SettingService : ISettingService
{
    private readonly ISettingRepository _settingRepository;
    private readonly IMemoryCache _cache;

    public SettingService(ISettingRepository settingRepository, IMemoryCache cache)
    {
        _settingRepository = settingRepository;
        _cache = cache;
    }

    public async Task<GetSettingResponse> GetSetting(long userId)
    {
        StatusEnum status = StatusEnum.Successed;
        SettingDto data;
        try
        {
            var setting = await _settingRepository.GetSetting(userId);
            data = new SettingDto(setting);
        }
        finally
        {
            await _settingRepository.ReleaseResource();
        }
        return new GetSettingResponse(userId, data, status, _cache);
    }

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
