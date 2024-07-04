using Azure.Core;
using DemoBuildCoreProject.Business.IService;
using DemoBuildCoreProject.Interface;
using DemoBuildCoreProject.Repository;
using HappyBookingShare.Common;
using HappyBookingShare.Entities;
using HappyBookingShare.Model;
using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.Setting;
using HappyBookingShare.Response.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoBuildCoreProject.Business;

public class SettingService : ISettingService
{
    private readonly ISettingRepository _settingRepository;

    public SettingService(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
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
        return new GetSettingResponse(data, status);
    }

    public async Task<SaveSettingResponse> SaveSetting(SaveSettingRequest request, long userId)
    {
        try
        {
            StatusEnum status = StatusEnum.Successed;
            var model = new SettingModel(userId, request.LanguageCode);
            var result = await _settingRepository.SaveSetting(model);
            return new SaveSettingResponse(result, status);
        }
        finally
        {
            await _settingRepository.ReleaseResource();
        }
    }
}
