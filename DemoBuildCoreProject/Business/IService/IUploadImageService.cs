using HappyBookingShare.Response.ImageUpload;

namespace DemoBuildCoreProject.Business.IService;

public interface IUploadImageService
{
    Task<UploadImageResponse> UploadImageAsync(IFormFile image, long userId);

    Task<DeleteImageResponse> DeleteImageAsync(string deleteHash, long userId);
}
