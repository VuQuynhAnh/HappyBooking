using HappyBookingShare.Response.Setting;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Setting;

public interface IGetSettingByUserIdUseCase
{
    Task<GetSettingResponse> GetSettingByUserId(long userId);
}
