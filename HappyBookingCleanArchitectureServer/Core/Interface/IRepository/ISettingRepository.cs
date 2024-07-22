using HappyBookingShare.Model;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IRepository;

public interface ISettingRepository : IRepositoryBase
{
    Task<bool> SaveSetting(SettingModel model);

    Task<SettingModel> GetSetting(long userId);
}
