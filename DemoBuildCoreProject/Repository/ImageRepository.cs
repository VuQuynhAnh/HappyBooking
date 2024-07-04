using DemoBuildCoreProject.DBContext;
using DemoBuildCoreProject.Interface;
using Microsoft.EntityFrameworkCore;

namespace DemoBuildCoreProject.Repository;

public class ImageRepository : IImageRepository
{
    private readonly DataContext _context;

    public ImageRepository(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// DeleteImage
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteImage(string imageUrl, long userId)
    {
        var entity = await _context.ImageManagementRepository.FirstOrDefaultAsync(item => item.ImageLink == imageUrl
                                                                                          && item.IsDeleted == 0);
        if (entity == null)
        {
            return false;
        }
        entity.IsDeleted = 0;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedId = userId;
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// UploadImage
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> UploadImage(string imageUrl, long userId)
    {
        var entity = await _context.ImageManagementRepository.FirstOrDefaultAsync(item => item.ImageLink == imageUrl
                                                                                          && item.IsDeleted == 0);

        if (entity == null)
        {
            entity = new();
            entity.Id = 0;
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedId = userId;
        }
        entity.ImageLink = imageUrl;
        entity.Status = 0;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedId = userId;
        if (entity.Id == 0)
        {
            _context.ImageManagementRepository.Add(entity);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// UsedImage
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> UsedImage(string imageUrl, long userId)
    {
        var entity = await _context.ImageManagementRepository.FirstOrDefaultAsync(item => item.ImageLink == imageUrl
                                                                                          && item.IsDeleted == 0);
        if (entity == null)
        {
            return false;
        }
        entity.Status = 1;
        entity.UpdatedDate = DateTime.UtcNow;
        entity.UpdatedId = userId;
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// GetClearImageList
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetClearImageList()
    {
        var result = await _context.ImageManagementRepository.Where(item => item.IsDeleted == 1
                                                                            || (item.Status == 0
                                                                                && item.CreatedDate.AddHours(3) <= DateTime.UtcNow))
                                                              .Select(item => item.ImageLink)
                                                              .ToListAsync();

        return result;
    }

    /// <summary>
    /// ClearImageList
    /// </summary>
    /// <param name="imageLinkList"></param>
    /// <returns></returns>
    public async Task<bool> ClearImageList(List<string> imageLinkList)
    {
        var imageDeletedList = await _context.ImageManagementRepository.Where(item => imageLinkList.Contains(item.ImageLink))
                                                                       .ToListAsync();
        _context.ImageManagementRepository.RemoveRange(imageDeletedList);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task ReleaseResource()
    {
        await _context.DisposeAsync();
    }
}
