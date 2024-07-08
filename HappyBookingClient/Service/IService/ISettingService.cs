using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;

namespace HappyBookingClient.Service.IService;

public interface ISettingService : IBaseApiService
{
    Task<GetSettingResponse?> GetSetting();

    Task<SaveSettingResponse?> SaveSetting(SaveSettingRequest request);
}
