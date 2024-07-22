namespace HappyBookingCleanArchitectureServer.Core.Interface.IRepository;

public interface IImageRepository : IRepositoryBase
{
    Task<bool> UploadImage(string imageUrl, long userId);

    Task<bool> DeleteImage(string imageUrl, long userId);

    Task<bool> UsedImage(string imageUrl, long userId);

    Task<List<string>> GetClearImageList();

    Task<bool> ClearImageList(List<string> imageLinkList);
}
