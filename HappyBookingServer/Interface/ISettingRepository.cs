using HappyBookingShare.Model;

namespace HappyBookingServer.Interface;

public interface ISettingRepository : IRepositoryBase
{
    Task<bool> SaveSetting(SettingModel model);

    Task<SettingModel> GetSetting(long userId);
}
