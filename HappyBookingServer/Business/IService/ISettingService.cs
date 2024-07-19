using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;

namespace DemoBuildCoreProject.Business.IService;

public interface ISettingService
{
    Task<SaveSettingResponse> SaveSetting(SaveSettingRequest request, long userId);

    Task<GetSettingResponse> GetSetting(long userId);
}
