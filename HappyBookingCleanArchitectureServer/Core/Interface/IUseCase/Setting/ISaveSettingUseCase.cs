using HappyBookingShare.Request.Setting;
using HappyBookingShare.Response.Setting;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;

public interface ISaveSettingUseCase
{
    Task<SaveSettingResponse> SaveSetting(SaveSettingRequest request, long userId);
}
