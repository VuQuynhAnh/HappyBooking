namespace DemoBuildCoreProject.Interface;

public interface IImageRepository
{
    Task<bool> UploadImage(string imageUrl, long userId);

    Task<bool> DeleteImage(string imageUrl, long userId);
}
