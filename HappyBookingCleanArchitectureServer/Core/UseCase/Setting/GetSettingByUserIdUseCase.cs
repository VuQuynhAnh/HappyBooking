using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;
using HappyBookingShare.Common;
using HappyBookingShare.Response.Dtos;
using HappyBookingShare.Response.Setting;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Setting;

public class GetSettingByUserIdUseCase : IGetSettingByUserIdUseCase
{
    private readonly ISettingRepository _settingRepository;
    private readonly IMemoryCache _cache;

    public GetSettingByUserIdUseCase(ISettingRepository settingRepository, IMemoryCache cache)
    {
        _settingRepository = settingRepository;
        _cache = cache;
    }

    /// <summary>
    /// GetSettingByUserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<GetSettingResponse> GetSettingByUserId(long userId)
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
}
