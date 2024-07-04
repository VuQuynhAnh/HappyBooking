using DemoBuildCoreProject.DBContext;
using DemoBuildCoreProject.Interface;
using HappyBookingShare.Entities;
using HappyBookingShare.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoBuildCoreProject.Repository;

public class SettingRepository : ISettingRepository
{
    private readonly DataContext _context;

    public SettingRepository(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Save setting
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> SaveSetting(SettingModel model)
    {
        var setting = await _context.SettingRepository.FirstOrDefaultAsync(item => item.UserId == model.UserId
                                                                                   && item.IsDeleted == 0);
        if (setting == null)
        {
            setting = new Setting();
            setting.UserId = model.UserId;
            setting.CreatedId = model.UserId;
            setting.CreatedDate = DateTime.UtcNow;
        }
        setting.LanguageCode = model.LanguageCode;
        setting.UpdatedDate = DateTime.UtcNow;
        setting.CreatedId = model.UserId;
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Get setting
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<SettingModel> GetSetting(long userId)
    {
        var setting = await _context.SettingRepository.FirstOrDefaultAsync(item => item.UserId == userId
                                                                                   && item.IsDeleted == 0);
        return new SettingModel(setting ?? new());
    }

    public async Task ReleaseResource()
    {
        await _context.DisposeAsync();
    }
}
